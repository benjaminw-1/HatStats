using HarmonyLib;
using ProjectM;
using ProjectM.Network;
using Unity.Collections;
using Unity.Entities;
using ProjectM.Shared;
using Stunlock.Core;
using System.Collections.Generic;

namespace HatStats;

[HarmonyPatch(typeof(ReactToInventoryChangedSystem), "OnUpdate")]
public static class ReactToInventory
{
    // Track last known helmet state
    private static Dictionary<Entity, bool> _helmetEquipped = new();

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

                if (headItemEntity != Entity.Null &&
                    entityManager.Exists(headItemEntity) &&
                    entityManager.HasComponent<PrefabGUID>(headItemEntity))
                {
                    var guid = entityManager.GetComponentData<PrefabGUID>(headItemEntity);
                    hasHelmetNow = guid.Equals(HatStatConstants.HelmetPrefabGUID);
                }

                _helmetEquipped.TryGetValue(entity, out var hadHelmetBefore);

                if (hasHelmetNow && !hadHelmetBefore)
                {
                    Plugin.LogInstance.LogInfo($"[HatStats] Helmet equipped, applying buff to {entity.Index}");
                    BuffUtility.TryAddBuffViaSystem(entity, entity, HatStatConstants.HelmetStatBuffGUID);
                }
                else if (!hasHelmetNow && hadHelmetBefore)
                {
                    Plugin.LogInstance.LogInfo($"[HatStats] Helmet removed, removing buff from {entity.Index}");
                    BuffUtility.TryRemoveBuffViaSystem(entity,entity, HatStatConstants.HelmetStatBuffGUID);
                }

                _helmetEquipped[entity] = hasHelmetNow;
            }
        }
        finally
        {
            entities.Dispose();
        }
    }
}
