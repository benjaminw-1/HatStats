using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using VampireCommandFramework;
using BepInEx.Logging;
using Stunlock.Core;
using ProjectM.Shared;
using ProjectM;
using ProjectM.Scripting;
using ProjectM.Network;
using ProjectM.Gameplay.Scripting;
using ProjectM.Gameplay.Systems;
using HatStats.Systems;
using Unity.Collections;


namespace HatStats;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("gg.deca.VampireCommandFramework")]
public class Plugin : BasePlugin
{
    
    internal static Harmony _harmony;
    public static ManualLogSource LogInstance { get; private set; }

    public override void Load()
    {
        LogInstance = Log;
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} version {MyPluginInfo.PLUGIN_VERSION} is loaded!");

        _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        _harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());

        CommandRegistry.RegisterAll();
    }

    public override bool Unload()
    {
        CommandRegistry.UnregisterAssembly();
        _harmony?.UnpatchSelf();
        return true;
    }

    public readonly PrefabGUID random_buff = new(-1245007017);
    [Command("testbuff", description: "Example command from testbuff", adminOnly: true)]
    public void ExampleCommand(ChatCommandContext ctx, string someString, int num = 5, float num2 = 1.5f)
    {
        FixedString512Bytes msg = $"Lacho's mod is active";
        ServerChatUtils.SendSystemMessageToAllClients(Core.EntityManager, ref msg);
    }

    public void OnGameInitialized()
    {
        if (!HasLoaded())
        {
            Log.LogDebug("Attempt to initialize before everything has loaded.");
            return;
        }

        Core.Initialize();
    }

    private static bool HasLoaded()
    {
        // Hack, check to make sure that entities loaded enough because this function
        // will be called when the plugin is first loaded, when this will return 0
        // but also during reload when there is data to initialize with.
        var collectionSystem = Core.Server.GetExistingSystemManaged<PrefabCollectionSystem>();
        return collectionSystem?.SpawnableNameToPrefabGuidDictionary.Count > 0;
    }
}
