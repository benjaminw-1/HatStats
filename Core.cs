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
        ServerScriptMapper = Server.GetExistingSystemManaged<ServerScriptMapper>();
        PrefabCollectionSystem = Server.GetExistingSystemManaged<PrefabCollectionSystem>();
        ServerGameManager = ServerScriptMapper.GetServerGameManager();
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
    public static ModifyUnitStatBuff_DOTS phyisical_power = new()
    {
        StatType = UnitStatType.PhysicalPower,
        Value = -9,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS spell_power = new()
    {
        StatType = UnitStatType.SpellPower,
        Value = 10,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS cooldown_reduction = new()
    {
        StatType = UnitStatType.CooldownRecoveryRate,
        Value = 10,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS regeneration = new()
    {
        StatType = UnitStatType.PassiveHealthRegen,
        Value = 0.05f,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS spell_lifeleech = new()
    {
        StatType = UnitStatType.SpellLifeLeech,
        Value = 50,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS fire_resistance = new()
    {
        StatType = UnitStatType.FireResistance,
        Value = 15,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS sun_resistance = new()
    {
        StatType = UnitStatType.SunResistance,
        Value = 15,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS sun_charge_time = new()
    {
        StatType = UnitStatType.SunChargeTime,
        Value = 20,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS holy_resistance = new()
    {
        StatType = UnitStatType.HolyResistance,
        Value = 15,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS attack_speed = new()
    {
        StatType = UnitStatType.PrimaryAttackSpeed,
        Value = 20,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS bonus_physical_power = new()
    {
        StatType = UnitStatType.BonusPhysicalPower,
        Value = 10,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS resource_power = new()
    {
        StatType = UnitStatType.ResourcePower,
        Value = 10,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS garlic_resistance = new()
    {
        StatType = UnitStatType.GarlicResistance,
        Value = 15,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS silver_resistance = new()
    {
        StatType = UnitStatType.SilverResistance,
        Value = 15,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    
    public static ModifyUnitStatBuff_DOTS bonus_shapeshift_movementspeed = new()
    {
        StatType = UnitStatType.BonusShapeshiftMovementSpeed,
        Value = 20,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    
    public static ModifyUnitStatBuff_DOTS max_health_wolfhat = new()
    {
        StatType = UnitStatType.MaxHealth,
        Value = -110,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS movement_speed_wolfhat = new()
    {
        StatType = UnitStatType.MovementSpeed,
        Value = 1,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS resource_yield = new()
    {
        StatType = UnitStatType.ResourceYield,
        Value = 20,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };

    public static ModifyUnitStatBuff_DOTS t2_spell_power = new()
    {
        StatType = UnitStatType.SpellPower,
        Value = 6,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS t2_spell_crit_damage = new()
    {
        StatType = UnitStatType.SpellPower,
        Value = 10,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };

    public static ModifyUnitStatBuff_DOTS t3_spell_power = new()
    {
        StatType = UnitStatType.SpellPower,
        Value = 3,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS t3_spell_crit = new()
    {
        StatType = UnitStatType.SpellCriticalStrikeChance,
        Value = 8,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS t3_physical_power = new()
    {
        StatType = UnitStatType.PhysicalPower,
        Value = 3,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS t3_physical_crit = new()
    {
        StatType = UnitStatType.PhysicalCriticalStrikeChance,
        Value = 8,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS t3_minion_damage = new()
    {
        StatType = UnitStatType.MinionDamage,
        Value = 10,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };

    public static ModifyUnitStatBuff_DOTS t3_physical_resistance = new()
    {
        StatType = UnitStatType.PhysicalResistance,
        Value = 2,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };

    public static ModifyUnitStatBuff_DOTS t3_damage_against_vblood = new()
    {
        StatType = UnitStatType.DamageVsVBloods,
        Value = 1,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };

    public static ModifyUnitStatBuff_DOTS t3_damage_against_vampires = new()
    {
        StatType = UnitStatType.DamageVsVampires,
        Value = 1,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS physical_leech = new()
    {
        StatType = UnitStatType.PhysicalLifeLeech,
        Value = 50,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS damage_reduction = new()
    {
        StatType = UnitStatType.DamageReduction,
        Value = 4,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS millitia_physical_debuff = new()
    {
        StatType = UnitStatType.PhysicalPower,
        Value = -4,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };

}