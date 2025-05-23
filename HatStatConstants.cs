using ProjectM;
using Stunlock.Core;
using System.Collections.Generic;
namespace HatStats;

public static class HatStatConstants
{
    //public static readonly PrefabGUID HelmetPrefabGUID = new PrefabGUID(1375804543);
    //public static readonly PrefabGUID HelmetStatBuffGUID = new PrefabGUID(-1598161201);


    public static readonly Dictionary<PrefabGUID, PrefabGUID> HelmetBuffMap = new()
    {
        // { helmetPrefabGUID, buffPrefabGUID }
        { new PrefabGUID(1375804543), new PrefabGUID(-1598161201) }, // Straw Hat - 
        { new PrefabGUID(-152150271),  new PrefabGUID(-1598161201) },  // Bonnet  -
        { new PrefabGUID(-1721887666),  new PrefabGUID(-1598161201) }, // Maid's cap  -
        { new PrefabGUID(-1460281233),  new PrefabGUID(-1598161201) }, // Maid's scarf  -
        { new PrefabGUID(417648894),  new PrefabGUID(-1598161201) },   // Militia Helmet
        { new PrefabGUID(1364460757),  new PrefabGUID(-1598161201) }, // Rusted Helmet
        { new PrefabGUID(-353076115),  new PrefabGUID(-1598161201) }, // Footman's Helmet
        { new PrefabGUID(-1818243335),  new PrefabGUID(-1598161201) }, // Knight's Helmet
        { new PrefabGUID(1780339680),  new PrefabGUID(-1598161201) }, // Paladin's Helmet
        { new PrefabGUID(-1988816037),  new PrefabGUID(914043867) }, // Ashfolk Crown -855125670
        { new PrefabGUID(-2111388989),  new PrefabGUID(-1598161201) }, // Boneguard Mask
        { new PrefabGUID(403967307),  new PrefabGUID(-1598161201) }, // Scarecrow Mask
        { new PrefabGUID(974739126),  new PrefabGUID(-1598161201) }, // Vampire Hunter
        { new PrefabGUID(607559019),  new PrefabGUID(-1598161201) }, // Necromancer's Mitre
        { new PrefabGUID(-1071187362),  new PrefabGUID(-1598161201) }, // Pilgrim's Hat
        { new PrefabGUID(1707139699),  new PrefabGUID(-1598161201) }, // Deer Head
        { new PrefabGUID(-1169471531),  new PrefabGUID(-1598161201) }, // Wolf Head
        { new PrefabGUID(714007172),  new PrefabGUID(-1598161201) }, // Bear Head
        { new PrefabGUID(690259405),  new PrefabGUID(-1598161201) }, // Top Hat
        { new PrefabGUID(-1607893829),  new PrefabGUID(-1598161201) }, // Ashfolk Helmet
        { new PrefabGUID(-548847761),  new PrefabGUID(-1598161201) }, // Pope Mitre


    };
}
