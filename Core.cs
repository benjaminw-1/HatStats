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
using HatStats.Systems;



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

        if (!routine.active)
        {
            routine.StartCoroutine(routine.test());
            Plugin.LogInstance.LogInfo($"The routine has started. Yes Zak, it is only one this time.");
        }
        hasInitialized = true;

    }



    public static ModifyUnitStatBuff_DOTS regeneration = new()
    {
        StatType = UnitStatType.PassiveHealthRegen,
        Value = 1.5f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS spell_lifeleech = new()
    {
        StatType = UnitStatType.SpellLifeLeech,
        Value = 30,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS fire_resistance = new()
    {
        StatType = UnitStatType.FireResistance,
        Value = 10,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS sun_resistance = new()
    {
        StatType = UnitStatType.SunResistance,
        Value = 10,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };

    public static ModifyUnitStatBuff_DOTS holy_resistance = new()
    {
        StatType = UnitStatType.HolyResistance,
        Value = 10,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };

    public static ModifyUnitStatBuff_DOTS resource_power = new()
    {
        StatType = UnitStatType.ResourcePower,
        Value = 1.2f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS garlic_resistance = new()
    {
        StatType = UnitStatType.GarlicResistance,
        Value = 10,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS silver_resistance = new()
    {
        StatType = UnitStatType.SilverResistance,
        Value = 10,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    
    public static ModifyUnitStatBuff_DOTS bonus_shapeshift_movementspeed = new()
    {
        StatType = UnitStatType.BonusShapeshiftMovementSpeed,
        Value = 4,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    
    public static ModifyUnitStatBuff_DOTS max_health_wolfhat = new()
    {
        StatType = UnitStatType.MaxHealth,
        Value = 0.5f,
        ModificationType = ModificationType.Multiply,
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
        Value = 1.3f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };

    public static ModifyUnitStatBuff_DOTS t2_spell_power = new()
    {
        StatType = UnitStatType.SpellPower,
        Value = 1.10f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS t2_spell_crit_damage = new()
    {
        StatType = UnitStatType.SpellPower,
        Value = 1.1f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };

    public static ModifyUnitStatBuff_DOTS t3_spell_power = new()
    {
        StatType = UnitStatType.SpellPower,
        Value = 1.1f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS t3_spell_crit = new()
    {
        StatType = UnitStatType.SpellCriticalStrikeChance,
        Value = 1.2f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS t3_physical_power = new()
    {
        StatType = UnitStatType.PhysicalPower,
        Value = 1.1f,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS t3_physical_crit = new()
    {
        StatType = UnitStatType.PhysicalCriticalStrikeChance,
        Value = 1.2f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS t3_minion_damage = new()
    {
        StatType = UnitStatType.MinionDamage,
        Value = 1.075f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };

    public static ModifyUnitStatBuff_DOTS t3_damage_against_vblood = new()
    {
        StatType = UnitStatType.DamageVsVBloods,
        Value = 0.1f,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };

    public static ModifyUnitStatBuff_DOTS t3_damage_against_vampires = new()
    {
        StatType = UnitStatType.DamageVsVampires,
        Value = 0.03f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS physical_leech = new()
    {
        StatType = UnitStatType.PhysicalLifeLeech,
        Value = 15,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS damage_reduction = new()
    {
        StatType = UnitStatType.DamageReduction,
        Value = 2,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS millitia_physical_debuff = new()
    {
        StatType = UnitStatType.PhysicalPower,
        Value = 0.8f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };


    public static ModifyUnitStatBuff_DOTS weapon_skill_damage = new()
    {
        StatType = UnitStatType.WeaponSkillPower,
        Value = 1.15f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };

    public static ModifyUnitStatBuff_DOTS attack_speed= new()
    {
        StatType = UnitStatType.PrimaryAttackSpeed,
        Value = 1.05f,
        ModificationType = ModificationType.Multiply,
        AttributeCapType = AttributeCapType.Uncapped,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS t2_physical_power_bonus = new()
    {
        StatType = UnitStatType.BonusPhysicalPower,
        Value = 0.08f,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS t2_physical_power = new()
    {
        StatType = UnitStatType.PhysicalPower,
        Value = 1.25f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS knight_helmet_debuff = new()
    {
        StatType = UnitStatType.SpellPower,
        Value = 0.5f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS knight_helmet_flat_phys = new()
    {
        StatType = UnitStatType.PhysicalPower,
        Value = 2,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };

    public static ModifyUnitStatBuff_DOTS t2_damage_against_vblood = new()
    {
        StatType = UnitStatType.DamageVsVBloods,
        Value = 0.12f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };

    public static ModifyUnitStatBuff_DOTS t2_damage_against_vampires = new()
    {
        StatType = UnitStatType.DamageVsVampires,
        Value = 0.05f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS bonus_mount_movementspeed = new()
    {
        StatType = UnitStatType.BonusMountMovementSpeed,
        Value = 3f,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS spell_cd_recovery = new()
    {
        StatType = UnitStatType.SpellCooldownRecoveryRate,
        Value = 0.3f,
        ModificationType = ModificationType.Add,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };

    public static ModifyUnitStatBuff_DOTS t2_minion_damage = new()
    {
        StatType = UnitStatType.MinionDamage,
        Value = 1.2f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS health_recovery = new()
    {
        StatType = UnitStatType.HealthRecovery,
        Value = 1.05f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS max_health = new()
    {
        StatType = UnitStatType.MaxHealth,
        Value = 1.1f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
    public static ModifyUnitStatBuff_DOTS weapon_skill_damage_pope_mittre = new()
    {
        StatType = UnitStatType.WeaponSkillPower,
        Value = 1.08f,
        ModificationType = ModificationType.Multiply,
        Modifier = 1,
        Id = ModificationId.NewId(2)
    };
}