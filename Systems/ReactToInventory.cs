using HarmonyLib;
using ProjectM;
using ProjectM.Network;
using Unity.Collections;
using Unity.Entities;
using ProjectM.Shared;
using Stunlock.Core;
using System.Collections.Generic;
using Unity.Rendering;

namespace HatStats;

[HarmonyPatch(typeof(ReactToInventoryChangedSystem), "OnUpdate")]
public static class ReactToInventory
{
    // Track the last equipped helmet GUID per entity
    internal static Dictionary<Entity, PrefabGUID> _lastEquippedHelmet = new();

    public static void Prefix(ReactToInventoryChangedSystem __instance)
    {
        var entityManager = __instance.EntityManager;

        var query = entityManager.CreateEntityQuery(new EntityQueryDesc
        {
            All = new ComponentType[]
            {
                ComponentType.ReadOnly<Equipment>()
            }
        });

        var entities = query.ToEntityArray(Allocator.Temp);
        try
        {
            foreach (var entity in entities)
            {
                if (!entityManager.HasComponent<Equipment>(entity)) continue;

                var equipment = entityManager.GetComponentData<Equipment>(entity);
                var headItemEntity = equipment.ArmorHeadgearSlot.SlotEntity.GetEntityOnServer();

                bool hasHelmetNow = false;
                PrefabGUID helmetGuid = default;
                PrefabGUID buffGuid = default;

                if (headItemEntity != Entity.Null &&
                    entityManager.Exists(headItemEntity) &&
                    entityManager.HasComponent<PrefabGUID>(headItemEntity))
                {
                    helmetGuid = entityManager.GetComponentData<PrefabGUID>(headItemEntity);
                    hasHelmetNow = HatStatConstants.HelmetBuffMap.TryGetValue(helmetGuid, out buffGuid);

                    if (hasHelmetNow)
                    {
                        Plugin.LogInstance.LogInfo($"[HatStats] Equipped helmet {helmetGuid.GuidHash} mapped to buff {buffGuid.GuidHash}");
                    }
                    else
                    {
                        Plugin.LogInstance.LogInfo($"[HatStats] Helmet {helmetGuid.GuidHash} not found in HelmetBuffMap.");
                    }
                }

                // Fetch previously equipped helmet
                _lastEquippedHelmet.TryGetValue(entity, out var previousHelmetGuid);
                bool helmetChanged = !helmetGuid.Equals(previousHelmetGuid);

                if (helmetChanged)
                {
                    // Remove old buff if previous helmet was tracked
                    if (HatStatConstants.HelmetBuffMap.TryGetValue(previousHelmetGuid, out var oldBuffGuid))
                    {
                        Plugin.LogInstance.LogInfo($"[HatStats] Helmet changed, removing old buff {oldBuffGuid.GuidHash} from entity {entity.Index}");
                        BuffUtility.TryRemoveBuffViaSystem(entity, entity, oldBuffGuid);

                    }

                    // Apply new buff if a new helmet is equipped
                    if (hasHelmetNow)
                    {
                        Plugin.LogInstance.LogInfo($"[HatStats] Applying new buff {buffGuid.GuidHash} for helmet {helmetGuid.GuidHash} to entity {entity.Index}");
                        BuffUtility.TryAddBuffViaSystem(entity, entity, buffGuid);
                        _lastEquippedHelmet[entity] = helmetGuid;

                    //    Core.PrefabCollection._PrefabLookupMap.TryGetValue(buffGuid, out Entity test);

                    //    Core.PrefabCollection._PrefabGuidToNameDictionary.TryGetValue(buffGuid, out var name);
                    ////    Core.Log.LogInfo("===");
                    //    Core.Log.LogInfo($"{name}");
                    //    test.LogComponentTypes();

                    //    if (entity.TryGetComponent(out Buff stats)) continue;
                    //    Core.Log.LogInfo($"[Max stacks] = {stats.MaxStacks}");
                    //    Core.Log.LogInfo($"[OneInstancePerOwner] = {stats.OneInstancePerOwner}");
                    //    Core.Log.LogInfo($"[Stacks] = {stats.Stacks}");
                    //
                    }
                    else
                    {
                        _lastEquippedHelmet.Remove(entity); // No helmet equipped now
                    }
                }
            }
        }
        finally
        {
            entities.Dispose();
        }
    }
}
