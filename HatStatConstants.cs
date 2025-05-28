using ProjectM;
using Stunlock.Core;
using System;
using System.Collections.Generic;
namespace HatStats;

public static class HatStatConstants
{
    //public static readonly PrefabGUID HelmetPrefabGUID = new PrefabGUID(1375804543);
    //public static readonly PrefabGUID HelmetStatBuffGUID = new PrefabGUID(-1598161201);

    public static float HealthRegen = 1.5f;
    public static float SpellLifeLeech = 30f;
    public static float FireResistance = 10f;
    public static float SunResistance = 10f;
    public static float HolyResistance = 10f;
    public static float ResourcePower = 1.2f;
    public static float GarlicResistance = 10f;
    public static float SilverResistance = 10f;
    public static float BonusShapeshiftMovementSpeed = 4;
    public static float MaxHealthWolfHat = 0.5f;
    public static float MovementSpeedWolfHat = 1;
    public static float ResourceYield = 1.3f;
    public static float T2SpellPower = 1.1f;
    public static float T2SpellCriticalDamage = 1.1f;
    public static float T3SpellPower = 1.1f;
    public static float T3SpellCriticalChance = 1.2f;
    public static float T3PhysicalPower = 1.1f;
    public static float T3PhysicalCriticalChance = 1.2f;
    public static float T3MinionDamage = 1.075f;
    public static float T3DamageAgainstVBlood = 0.1f;
    public static float T3DamageAgainstVampires = 0.03f;
    public static float PhysicalLeech = 15f;
    public static float DamageReduction = 2f;
    public static float MillitiaPhysicalDebuff = 0.8f;
    public static float WeaponSkillDamage = 1.15f;
    public static float AttackSpeed = 1.05f;
    public static float T2PhysicalPowerBonus = 0.08f;
    public static float KnightHelmetDebuff = 0.5f;
    public static float KnightHelmetFlatPhysicalPower = 2;
    public static float T2DamageAgainstVBlood = 0.12f;
    public static float T2DamageAgainstVampires = 0.05f;
    public static float BonusMountMovementSpeed = 3f;
    public static float SpellCooldownRecovery = 0.3f;
    public static float T2MinionDamage = 1.2f;
    public static float HealthRecovery = 1.05f;
    public static float MaxHealth = 1.1f;
    public static float WeaponSkillDamagePopeMittre = 1.08f;

    public static Dictionary<PrefabGUID, List<ModifyUnitStatBuff_DOTS>> HelmetBuffMap;
    public static Dictionary<string, Action<float>> StatMap;
    public static ModifyUnitStatBuff_DOTS regeneration;
    public static ModifyUnitStatBuff_DOTS spell_lifeleech;
    public static ModifyUnitStatBuff_DOTS fire_resistance;
    public static ModifyUnitStatBuff_DOTS sun_resistance;
    public static ModifyUnitStatBuff_DOTS holy_resistance;
    public static ModifyUnitStatBuff_DOTS resource_power;
    public static ModifyUnitStatBuff_DOTS garlic_resistance;
    public static ModifyUnitStatBuff_DOTS silver_resistance;
    public static ModifyUnitStatBuff_DOTS bonus_shapeshift_movementspeed;
    public static ModifyUnitStatBuff_DOTS max_health_wolfhat;
    public static ModifyUnitStatBuff_DOTS movement_speed_wolfhat;
    public static ModifyUnitStatBuff_DOTS resource_yield;
    public static ModifyUnitStatBuff_DOTS t2_spell_power;
    public static ModifyUnitStatBuff_DOTS t2_spell_crit_damage;
    public static ModifyUnitStatBuff_DOTS t3_spell_power;
    public static ModifyUnitStatBuff_DOTS t3_spell_crit;
    public static ModifyUnitStatBuff_DOTS t3_physical_power;
    public static ModifyUnitStatBuff_DOTS t3_physical_crit;
    public static ModifyUnitStatBuff_DOTS t3_minion_damage;
    public static ModifyUnitStatBuff_DOTS t3_damage_against_vblood;
    public static ModifyUnitStatBuff_DOTS t3_damage_against_vampires;
    public static ModifyUnitStatBuff_DOTS physical_leech;
    public static ModifyUnitStatBuff_DOTS damage_reduction;
    public static ModifyUnitStatBuff_DOTS millitia_physical_debuff;
    public static ModifyUnitStatBuff_DOTS weapon_skill_damage;
    public static ModifyUnitStatBuff_DOTS attack_speed;
    public static ModifyUnitStatBuff_DOTS t2_physical_power_bonus;
    public static ModifyUnitStatBuff_DOTS knight_helmet_debuff;
    public static ModifyUnitStatBuff_DOTS knight_helmet_flat_phys;
    public static ModifyUnitStatBuff_DOTS t2_damage_against_vblood;
    public static ModifyUnitStatBuff_DOTS t2_damage_against_vampires;
    public static ModifyUnitStatBuff_DOTS bonus_mount_movementspeed;
    public static ModifyUnitStatBuff_DOTS spell_cd_recovery;
    public static ModifyUnitStatBuff_DOTS t2_minion_damag;
    public static ModifyUnitStatBuff_DOTS health_recovery;
    public static ModifyUnitStatBuff_DOTS max_health;
    public static ModifyUnitStatBuff_DOTS weapon_skill_damage_pope_mittre;



    static HatStatConstants()
    {
        initializeBuff();
    }


    public static void initializeBuff()
    {
        ModifyUnitStatBuff_DOTS regeneration = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.PassiveHealthRegen,
            Value = HatStatConstants.HealthRegen,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS spell_lifeleech = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.SpellLifeLeech,
            Value = HatStatConstants.SpellLifeLeech,
            ModificationType = ModificationType.Add,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS fire_resistance = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.FireResistance,
            Value = HatStatConstants.FireResistance,
            ModificationType = ModificationType.Add,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS sun_resistance = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.SunResistance,
            Value = HatStatConstants.SunResistance,
            ModificationType = ModificationType.Add,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };

        ModifyUnitStatBuff_DOTS holy_resistance = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.HolyResistance,
            Value = HatStatConstants.HolyResistance,
            ModificationType = ModificationType.Add,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };

        ModifyUnitStatBuff_DOTS resource_power = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.ResourcePower,
            Value = HatStatConstants.ResourcePower,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS garlic_resistance = new ModifyUnitStatBuff_DOTS()
        {
            StatType = UnitStatType.GarlicResistance,
            Value = HatStatConstants.GarlicResistance,
            ModificationType = ModificationType.Add,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS silver_resistance = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.SilverResistance,
            Value = HatStatConstants.SilverResistance,
            ModificationType = ModificationType.Add,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };

        ModifyUnitStatBuff_DOTS bonus_shapeshift_movementspeed = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.BonusShapeshiftMovementSpeed,
            Value = HatStatConstants.BonusShapeshiftMovementSpeed,
            ModificationType = ModificationType.Add,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };

        ModifyUnitStatBuff_DOTS max_health_wolfhat = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.MaxHealth,
            Value = HatStatConstants.MaxHealthWolfHat,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS movement_speed_wolfhat = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.MovementSpeed,
            Value = HatStatConstants.MovementSpeedWolfHat,
            ModificationType = ModificationType.Add,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS resource_yield = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.ResourceYield,
            Value = HatStatConstants.ResourceYield,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };

        ModifyUnitStatBuff_DOTS t2_spell_power = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.SpellPower,
            Value = HatStatConstants.T2SpellPower,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS t2_spell_crit_damage = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.SpellPower,
            Value = HatStatConstants.T2SpellCriticalDamage,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };

        ModifyUnitStatBuff_DOTS t3_spell_power = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.SpellPower,
            Value = HatStatConstants.T3SpellPower,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS t3_spell_crit = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.SpellCriticalStrikeChance,
            Value = HatStatConstants.T3SpellCriticalChance,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS t3_physical_power = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.PhysicalPower,
            Value = HatStatConstants.T3PhysicalPower,
            ModificationType = ModificationType.Add,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS t3_physical_crit = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.PhysicalCriticalStrikeChance,
            Value = HatStatConstants.T3PhysicalCriticalChance,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS t3_minion_damage = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.MinionDamage,
            Value = HatStatConstants.T3MinionDamage,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };

        ModifyUnitStatBuff_DOTS t3_damage_against_vblood = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.DamageVsVBloods,
            Value = HatStatConstants.T3DamageAgainstVBlood,
            ModificationType = ModificationType.Add,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };

        ModifyUnitStatBuff_DOTS t3_damage_against_vampires = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.DamageVsVampires,
            Value = HatStatConstants.T3DamageAgainstVampires,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS physical_leech = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.PhysicalLifeLeech,
            Value = HatStatConstants.PhysicalLeech,
            ModificationType = ModificationType.Add,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS damage_reduction = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.DamageReduction,
            Value = HatStatConstants.DamageReduction,
            ModificationType = ModificationType.Add,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS millitia_physical_debuff = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.PhysicalPower,
            Value = HatStatConstants.MillitiaPhysicalDebuff,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };


        ModifyUnitStatBuff_DOTS weapon_skill_damage = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.WeaponSkillPower,
            Value = HatStatConstants.WeaponSkillDamage,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };

        ModifyUnitStatBuff_DOTS attack_speed = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.PrimaryAttackSpeed,
            Value = HatStatConstants.AttackSpeed,
            ModificationType = ModificationType.Multiply,
            AttributeCapType = AttributeCapType.Uncapped,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS t2_physical_power_bonus = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.BonusPhysicalPower,
            Value = HatStatConstants.T2PhysicalPowerBonus,
            ModificationType = ModificationType.Add,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        //public static ModifyUnitStatBuff_DOTS t2_physical_power = new()
        //{
        //    StatType = UnitStatType.PhysicalPower,
        //    Value = 1.25f,
        //    ModificationType = ModificationType.Multiply,
        //    Modifier = 1,
        //    Id = ModificationId.NewId(2)
        //};
        ModifyUnitStatBuff_DOTS knight_helmet_debuff = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.SpellPower,
            Value = HatStatConstants.KnightHelmetDebuff,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS knight_helmet_flat_phys = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.PhysicalPower,
            Value = HatStatConstants.KnightHelmetFlatPhysicalPower,
            ModificationType = ModificationType.Add,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };

        ModifyUnitStatBuff_DOTS t2_damage_against_vblood = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.DamageVsVBloods,
            Value = HatStatConstants.T2DamageAgainstVBlood,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };

        ModifyUnitStatBuff_DOTS t2_damage_against_vampires = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.DamageVsVampires,
            Value = HatStatConstants.T2DamageAgainstVampires,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS bonus_mount_movementspeed = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.BonusMountMovementSpeed,
            Value = HatStatConstants.BonusMountMovementSpeed,
            ModificationType = ModificationType.Add,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS spell_cd_recovery = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.SpellCooldownRecoveryRate,
            Value = HatStatConstants.SpellCooldownRecovery,
            ModificationType = ModificationType.Add,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };

        ModifyUnitStatBuff_DOTS t2_minion_damage = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.MinionDamage,
            Value = HatStatConstants.T2MinionDamage,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS health_recovery = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.HealthRecovery,
            Value = HatStatConstants.HealthRecovery,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS max_health = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.MaxHealth,
            Value = HatStatConstants.MaxHealth,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };
        ModifyUnitStatBuff_DOTS weapon_skill_damage_pope_mittre = new ModifyUnitStatBuff_DOTS
        {
            StatType = UnitStatType.WeaponSkillPower,
            Value = HatStatConstants.WeaponSkillDamagePopeMittre,
            ModificationType = ModificationType.Multiply,
            Modifier = 1,
            Id = ModificationId.NewId(2)
        };

        HelmetBuffMap = new Dictionary<PrefabGUID, List<ModifyUnitStatBuff_DOTS>>
        {
            { new PrefabGUID(1375804543), new List<ModifyUnitStatBuff_DOTS>{resource_power, resource_yield} }, // Straw Hat - Resistances 
            { new PrefabGUID(-152150271),  new List<ModifyUnitStatBuff_DOTS>{t2_spell_power, regeneration, t2_spell_crit_damage} },  // Bonnet  - Movement speed 
            { new PrefabGUID(-1721887666),  new List<ModifyUnitStatBuff_DOTS>{t3_spell_power, spell_cd_recovery} }, // Maid's cap  -
            //{ new PrefabGUID(-1460281233),  new PrefabGUID(-1598161201) }, // Maid's scarf  -
            { new PrefabGUID(417648894),  new List<ModifyUnitStatBuff_DOTS>{millitia_physical_debuff, damage_reduction} },   // Militia Helmet
            { new PrefabGUID(1364460757),  new List<ModifyUnitStatBuff_DOTS>{t3_physical_power, t3_physical_crit, physical_leech}}, // Rusted Helmet
            { new PrefabGUID(-353076115),  new List<ModifyUnitStatBuff_DOTS>{weapon_skill_damage, t3_physical_power} }, // Footman's Helmet
            { new PrefabGUID(-1818243335),  new List<ModifyUnitStatBuff_DOTS>{t2_physical_power_bonus,knight_helmet_flat_phys, knight_helmet_debuff } }, // Knight's Helmet
            //{ new PrefabGUID(1780339680),  new PrefabGUID(-1598161201) }, // Paladin's Helmet
            //{ new PrefabGUID(-1988816037),  new List<ModifyUnitStatBuff_DOTS>{phyisical_power} }, // Ashfolk Crown -855125670
            { new PrefabGUID(-2111388989),  new List<ModifyUnitStatBuff_DOTS>{t3_minion_damage, regeneration } }, // Boneguard Mask
            { new PrefabGUID(403967307),  new List<ModifyUnitStatBuff_DOTS>{ attack_speed } }, // Scarecrow Mask
            { new PrefabGUID(974739126),  new List<ModifyUnitStatBuff_DOTS>{t2_damage_against_vampires, t2_damage_against_vblood, sun_resistance}  }, // Vampire Hunter
            { new PrefabGUID(607559019),  new List<ModifyUnitStatBuff_DOTS>{t3_spell_power, t3_spell_crit, spell_lifeleech} }, // Necromancer's Mitre
            { new PrefabGUID(-1071187362),  new List<ModifyUnitStatBuff_DOTS>{t3_damage_against_vampires, t3_damage_against_vblood}  }, // Pilgrim's Hat
            { new PrefabGUID(1707139699),  new List<ModifyUnitStatBuff_DOTS>{bonus_mount_movementspeed }}, // Deer Head
            { new PrefabGUID(-1169471531), new List<ModifyUnitStatBuff_DOTS>{bonus_shapeshift_movementspeed, max_health_wolfhat, movement_speed_wolfhat} }, // Wolf Head
            { new PrefabGUID(714007172), new List<ModifyUnitStatBuff_DOTS>{fire_resistance, sun_resistance, silver_resistance, garlic_resistance, holy_resistance} }, // Bear Head
            //{ new PrefabGUID(690259405),  new PrefabGUID(-1598161201) }, // Top Hat
            //{ new PrefabGUID(-1607893829),  new PrefabGUID(-1598161201) }, // Ashfolk Helmet
            { new PrefabGUID(-548847761),  new List<ModifyUnitStatBuff_DOTS>{ weapon_skill_damage, health_recovery, max_health} }, // Pope Mitre
            { new PrefabGUID(-1797796642),  new List<ModifyUnitStatBuff_DOTS>{t2_minion_damage, holy_resistance, spell_cd_recovery } },
        };

        StatMap = new Dictionary<string, Action<float>>
        {
            {"health_regen",                        value => HealthRegen = value },
            {"spell_life_leech",                    value => SpellLifeLeech = value },
            {"fire_resistance",                     value => FireResistance = value },
            {"sun_resistance",                      value => SunResistance = value },
            {"garlic_resistance",                   value => GarlicResistance = value },
            {"holy_resistance",                     value => HolyResistance = value },
            {"silver_resistance",                   value => SilverResistance = value },
            {"resource_power",                      value => ResourcePower = value },
            {"bonus_shapeshift_movement_speed",     value => BonusShapeshiftMovementSpeed = value },
            {"max_health_wolf_hat",                 value => MaxHealthWolfHat = value },
            {"movement_speed_wolf_hat",             value => MovementSpeedWolfHat = value },
            {"resource_yield",                      value => ResourceYield = value },
            {"t2_spell_power",                      value => T2SpellPower = value },
            {"t2_spell_critical_damage",            value => T2SpellCriticalDamage = value },
            {"t3_spell_power",                      value => T3SpellPower = value },
            {"t3_spell_critical_chance",            value => T3SpellCriticalChance = value },
            {"t3_physical_power",                   value => T3PhysicalPower = value },
            {"t3_physical_critical_chance",         value => T3PhysicalCriticalChance = value },
            {"t3_minion_damage",                    value => T3MinionDamage = value },
            {"t3_damage_against_vblood",            value => T3DamageAgainstVBlood = value },
            {"t3_damage_against_vampires",          value => T3DamageAgainstVampires = value },
            {"physical_leech",                      value => PhysicalLeech = value },
            {"damage_reduction",                    value => DamageReduction = value },
            {"t2_damage_against_vblood",            value => T2DamageAgainstVBlood = value },
            {"t2_damage_against_vampires",          value => T2DamageAgainstVampires= value },
            {"millitia_physical_debuff",            value => MillitiaPhysicalDebuff = value },
            {"weapon_skill_damage",                 value => WeaponSkillDamage = value },
            {"attack_speed",                        value => AttackSpeed = value },
            {"t2_physical_power_bonus",             value => T2PhysicalPowerBonus = value },
            {"knight_helmet_debuff",                value => KnightHelmetDebuff = value },
            {"knight_helmet_flat_physical_power",   value => KnightHelmetFlatPhysicalPower = value },
            {"bonus_mount_movement_speed",          value => BonusMountMovementSpeed = value },
            {"spell_cooldown_recovery",             value => SpellCooldownRecovery = value },
            {"t2_minion_damage",                    value => T2MinionDamage = value },
            {"health_recovery",                     value => HealthRecovery = value },
            {"max_health",                          value => MaxHealth = value },
            {"weapon_skill_damage_pope_mittre",     value => WeaponSkillDamagePopeMittre = value }
        };
    }

    //public static float HealthRegen = 1.5f;
    //public static float SpellLifeLeech = 30f;
    //public static float FireResistance = 10f;
    //public static float SunResistance = 10f;
    //public static float HolyResistance = 10f;
    //public static float ResourcePower = 1.2f;
    //public static float GarlicResistance = 10f;
    //public static float SilverResistance = 10f;
    //public static float BonusShapeshiftMovementSpeed = 4;
    //public static float MaxHealthWolfHat = 0.5f;
    //public static float MovementSpeedWolfHat = 1;
    //public static float ResourceYield = 1.3f;
    //public static float T2SpellPower = 1.1f;
    //public static float T2SpellCriticalDamage = 1.1f;
    //public static float T3SpellPower = 1.1f;
    //public static float T3SpellCriticalChance = 1.2f;
    //public static float T3PhysicalPower = 1.1f;
    //public static float T3PhysicalCriticalChance = 1.2f;
    //public static float T3MinionDamage = 1.075f;
    //public static float T3DamageAgainstVBlood = 0.1f;
    //public static float T3DamageAgainstVampires = 0.03f;
    //public static float PhysicalLeech = 15f;
    //public static float DamageReduction = 2f;
    //public static float MillitiaPhysicalDebuff = 0.8f;
    //public static float WeaponSkillDamage = 1.15f;
    //public static float AttackSpeed = 1.05f;
    //public static float T2PhysicalPowerBonus = 0.08f;
    //public static float KnightHelmetDebuff = 0.5f;
    //public static float KnightHelmetFlatPhysicalPower = 2;
    //public static float T2DamageAgainstVBlood = 0.12f;
    //public static float T2DamageAgainstVampires = 0.05f;
    //public static float BonusMountMovementSpeed = 3f;
    //public static float SpellCooldownRecovery = 0.3f;
    //public static float T2MinionDamage = 1.2f;
    //public static float HealthRecovery = 1.05f;
    //public static float MaxHealth = 1.1f;
    //public static float WeaponSkillDamagePopeMittre = 1.08f;

    //public static readonly Dictionary<string, Action<float>> StatMap = new()
    //{
    //    {"health_regen",                        value => HealthRegen = value },
    //    {"spell_life_leech",                    value => SpellLifeLeech = value },
    //    {"fire_resistance",                     value => FireResistance = value },
    //    {"sun_resistance",                      value => SunResistance = value },
    //    {"garlic_resistance",                   value => GarlicResistance = value },
    //    {"holy_resistance",                     value => HolyResistance = value },
    //    {"silver_resistance",                   value => SilverResistance = value },
    //    {"resource_power",                      value => ResourcePower = value },
    //    {"bonus_shapeshift_movement_speed",     value => BonusShapeshiftMovementSpeed = value },
    //    {"max_health_wolf_hat",                 value => MaxHealthWolfHat = value },
    //    {"movement_speed_wolf_hat",             value => MovementSpeedWolfHat = value },
    //    {"resource_yield",                      value => ResourceYield = value },
    //    {"t2_spell_power",                      value => T2SpellPower = value },
    //    {"t2_spell_critical_damage",            value => T2SpellCriticalDamage = value },
    //    {"t3_spell_power",                      value => T3SpellPower = value },
    //    {"t3_spell_critical_chance",            value => T3SpellCriticalChance = value },
    //    {"t3_physical_power",                   value => T3PhysicalPower = value },
    //    {"t3_physical_critical_chance",         value => T3PhysicalCriticalChance = value },
    //    {"t3_minion_damage",                    value => T3MinionDamage = value },
    //    {"t3_damage_against_vblood",            value => T3DamageAgainstVBlood = value },
    //    {"t3_damage_against_vampires",          value => T3DamageAgainstVampires = value },
    //    {"physical_leech",                      value => PhysicalLeech = value },
    //    {"damage_reduction",                    value => DamageReduction = value },
    //    {"t2_damage_against_vblood",            value => T2DamageAgainstVBlood = value },
    //    {"t2_damage_against_vampires",          value => T2DamageAgainstVampires= value },
    //    {"millitia_physical_debuff",            value => MillitiaPhysicalDebuff = value },
    //    {"weapon_skill_damage",                 value => WeaponSkillDamage = value },
    //    {"attack_speed",                        value => AttackSpeed = value },
    //    {"t2_physical_power_bonus",             value => T2PhysicalPowerBonus = value },
    //    {"knight_helmet_debuff",                value => KnightHelmetDebuff = value },
    //    {"knight_helmet_flat_physical_power",   value => KnightHelmetFlatPhysicalPower = value },
    //    {"bonus_mount_movement_speed",          value => BonusMountMovementSpeed = value },
    //    {"spell_cooldown_recovery",             value => SpellCooldownRecovery = value },
    //    {"t2_minion_damage",                    value => T2MinionDamage = value },
    //    {"health_recovery",                     value => HealthRecovery = value },
    //    {"max_health",                          value => MaxHealth = value },
    //    {"weapon_skill_damage_pope_mittre",     value => WeaponSkillDamagePopeMittre = value }
    //};

}
