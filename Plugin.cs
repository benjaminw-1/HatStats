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


namespace HatStats;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("gg.deca.VampireCommandFramework")]
[BepInDependency("gg.deca.Bloodstone")]
[Bloodstone.API.Reloadable]
public class Plugin : BasePlugin
{
    
    Harmony _harmony;
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

    [Command("test", description: "Example command from hat stats", adminOnly: true)]
    public void ExampleCommand(ICommandContext ctx, string someString, int num = 5, float num2 = 1.5f)
    {
        ctx.Reply($"You passed in {someString} and {num} and {num2}");
    }


    public readonly PrefabGUID random_buff = new(792451792);
    [Command("testbuff", description: "Example command from testbuff", adminOnly: false)]
    public void ExampleCommand(ChatCommandContext ctx, string someString, int num = 5, float num2 = 1.5f)
    {
        PrefabGUID bear_buff = random_buff;


        ctx.Event.SenderCharacterEntity.TryApplyBuff(bear_buff);

        BuffUtility.TryGetBuff(Core.EntityManager, ctx.Event.SenderCharacterEntity, bear_buff, out var buffEntity);

        if (buffEntity.Has<ModifyUnitStatBuff_DOTS>())
        {
            buffEntity.Remove<ModifyUnitStatBuff_DOTS>();
        }

        var modifyStatBuffer = Core.EntityManager.AddBuffer<ModifyUnitStatBuff_DOTS>(buffEntity);

        modifyStatBuffer.Clear();

        //Core.whatever is not valid, make ur own struct above the function
        modifyStatBuffer.Add(Core.speed);
        //modifyStatBuffer.Add(Core.SiegePower);

        //this makes the buff permanent
        if (buffEntity.Has<LifeTime>())
        {
            var lifetime = buffEntity.Read<LifeTime>();
            lifetime.Duration = -1;
            lifetime.EndAction = LifeTimeEndAction.None;
            buffEntity.Write(lifetime);
        }

        // here im removing all the extra effects of the buff, this is case by case
        if (buffEntity.Has<RemoveBuffOnGameplayEvent>())
        {
            buffEntity.Remove<RemoveBuffOnGameplayEvent>();
        }
        if (buffEntity.Has<RemoveBuffOnGameplayEventEntry>())
        {
            buffEntity.Remove<RemoveBuffOnGameplayEventEntry>();
        }
        if (buffEntity.Has<Script_Modify_Combat_Movement_Buff_Data>())
        {
            buffEntity.Remove<Script_Modify_Combat_Movement_Buff_Data>();
        }
        if (buffEntity.Has<Script_Modify_Combat_Movement_Buff_State>())
        {
            buffEntity.Remove<Script_Modify_Combat_Movement_Buff_State>();
        }
        if (buffEntity.Has<DestroyOnGameplayEvent>())
        {
            buffEntity.Remove<DestroyOnGameplayEvent>();
        }
        if (buffEntity.Has<CreateGameplayEventsOnTick>())
        {
            buffEntity.Remove<CreateGameplayEventsOnTick>();
        }
        if (buffEntity.Has<ScriptSpawn>())
        {
            buffEntity.Remove<ScriptSpawn>();
        }

        //
        Core.PrefabCollectionSystem._PrefabGuidToNameDictionary.TryGetValue(bear_buff, out var name);
        Core.Log.LogInfo("===");
        Core.Log.LogInfo($"{name}");
        buffEntity.LogComponentTypes();


        if (buffEntity.TryGetBuffer<ModifyUnitStatBuff_DOTS>(out var buff_buffer))
        {
            foreach (var buff in buff_buffer)
            {
                Core.Log.LogInfo("===");
                Core.Log.LogInfo($"{buff.Id}");
                Core.Log.LogInfo($"{buff.StatType}");

            }
        }


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
