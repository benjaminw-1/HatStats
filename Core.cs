using ProjectM;
using UnityEngine;
using Unity.Entities;
using Stunlock.Core;
using BepInEx.Logging;
using ProjectM.Shared;
using ProjectM.Network;
using HarmonyLib;
using ProjectM.Physics;
using ProjectM.Scripting;



namespace HatStats;


internal static class Core
{

    public static World Server { get; } = GetWorld("Server") ?? throw new System.Exception("There is no Server world (yet). Did you install a server mod on the client?");

    public static EntityManager EntityManager { get; } = Server.EntityManager;
    public static PrefabCollectionSystem PrefabCollection => Server.GetExistingSystemManaged<PrefabCollectionSystem>();
    public static PrefabCollectionSystem PrefabCollectionSystem { get; internal set; }
    public static ManualLogSource Log => Plugin.LogInstance;
    public static ServerGameManager ServerGameManager { get; internal set; }
    public static ServerScriptMapper ServerScriptMapper { get; internal set; }
    public static DebugEventsSystem DebugEventsSystem { get; internal set; }

    private static World GetWorld(string name)
    {
        foreach (var world in World.s_AllWorlds)
        {
            if (world.Name == name)
            {
                return world;
            }
        }

        return null;
    }
    public static bool hasInitialized = false;
    public static void Initialize()
    {
        
        if (hasInitialized) return;

        // Initialize utility services
        PrefabCollectionSystem = Server.GetExistingSystemManaged<PrefabCollectionSystem>();
        ServerGameManager = ServerScriptMapper.GetServerGameManager();
        ServerScriptMapper = Server.GetExistingSystemManaged<ServerScriptMapper>();
        DebugEventsSystem = Server.GetExistingSystemManaged<DebugEventsSystem>();
        // GameplayEvents = Server.GetExistingSystemManaged<GameplayEventsSystem>();


        hasInitialized = true;
    }

    public static ModifyUnitStatBuff_DOTS speed = new()
    {
        StatType = UnitStatType.MovementSpeed,
        Value = 50,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
}