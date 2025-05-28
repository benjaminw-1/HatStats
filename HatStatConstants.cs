using ProjectM;
using Stunlock.Core;
using System.Collections.Generic;
namespace HatStats;

public static class HatStatConstants
{
    //public static readonly PrefabGUID HelmetPrefabGUID = new PrefabGUID(1375804543);
    //public static readonly PrefabGUID HelmetStatBuffGUID = new PrefabGUID(-1598161201);


    public static readonly Dictionary<PrefabGUID, List<ModifyUnitStatBuff_DOTS>> HelmetBuffMap = new()
    {
        // { helmetPrefabGUID, buffPrefabGUID }
        { new PrefabGUID(1375804543), new List<ModifyUnitStatBuff_DOTS>{Core.resource_power, Core.resource_yield} }, // Straw Hat - Resistances 
        { new PrefabGUID(-152150271),  new List<ModifyUnitStatBuff_DOTS>{Core.t2_spell_power, Core.regeneration, Core.t2_spell_crit_damage} },  // Bonnet  - Movement speed 
        { new PrefabGUID(-1721887666),  new List<ModifyUnitStatBuff_DOTS>{Core.t3_spell_power, Core.spell_cd_recovery} }, // Maid's cap  -
        //{ new PrefabGUID(-1460281233),  new PrefabGUID(-1598161201) }, // Maid's scarf  -
        { new PrefabGUID(417648894),  new List<ModifyUnitStatBuff_DOTS>{Core.millitia_physical_debuff, Core.damage_reduction} },   // Militia Helmet
        { new PrefabGUID(1364460757),  new List<ModifyUnitStatBuff_DOTS>{Core.t3_physical_power, Core.t3_physical_crit, Core.physical_leech}}, // Rusted Helmet
        { new PrefabGUID(-353076115),  new List<ModifyUnitStatBuff_DOTS>{Core.weapon_skill_damage, Core.t3_physical_power} }, // Footman's Helmet
        { new PrefabGUID(-1818243335),  new List<ModifyUnitStatBuff_DOTS>{Core.t2_physical_power_bonus,Core.knight_helmet_flat_phys, Core.knight_helmet_debuff } }, // Knight's Helmet
        //{ new PrefabGUID(1780339680),  new PrefabGUID(-1598161201) }, // Paladin's Helmet
        //{ new PrefabGUID(-1988816037),  new List<ModifyUnitStatBuff_DOTS>{Core.phyisical_power} }, // Ashfolk Crown -855125670
        { new PrefabGUID(-2111388989),  new List<ModifyUnitStatBuff_DOTS>{Core.t3_minion_damage, Core.regeneration } }, // Boneguard Mask
        { new PrefabGUID(403967307),  new List<ModifyUnitStatBuff_DOTS>{ Core.attack_speed } }, // Scarecrow Mask
        { new PrefabGUID(974739126),  new List<ModifyUnitStatBuff_DOTS>{Core.t2_damage_against_vampires, Core.t2_damage_against_vblood, Core.sun_resistance}  }, // Vampire Hunter
        { new PrefabGUID(607559019),  new List<ModifyUnitStatBuff_DOTS>{Core.t3_spell_power, Core.t3_spell_crit, Core.spell_lifeleech} }, // Necromancer's Mitre
        { new PrefabGUID(-1071187362),  new List<ModifyUnitStatBuff_DOTS>{Core.t3_damage_against_vampires, Core.t3_damage_against_vblood}  }, // Pilgrim's Hat
        { new PrefabGUID(1707139699),  new List<ModifyUnitStatBuff_DOTS>{Core.bonus_mount_movementspeed }}, // Deer Head
        { new PrefabGUID(-1169471531), new List<ModifyUnitStatBuff_DOTS>{Core.bonus_shapeshift_movementspeed, Core.max_health_wolfhat, Core.movement_speed_wolfhat} }, // Wolf Head
        { new PrefabGUID(714007172), new List<ModifyUnitStatBuff_DOTS>{Core.fire_resistance, Core.sun_resistance, Core.silver_resistance, Core.garlic_resistance, Core.holy_resistance} }, // Bear Head
        //{ new PrefabGUID(690259405),  new PrefabGUID(-1598161201) }, // Top Hat
        //{ new PrefabGUID(-1607893829),  new PrefabGUID(-1598161201) }, // Ashfolk Helmet
        { new PrefabGUID(-548847761),  new List<ModifyUnitStatBuff_DOTS>{ Core.weapon_skill_damage, Core.health_recovery, Core.max_health} }, // Pope Mitre
        { new PrefabGUID(-1797796642),  new List<ModifyUnitStatBuff_DOTS>{Core.t2_minion_damage, Core.holy_resistance, Core.spell_cd_recovery } }, // Razor Hood //



    };
}
