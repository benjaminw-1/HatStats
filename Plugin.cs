using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using VampireCommandFramework;
using BepInEx.Logging;

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
}
