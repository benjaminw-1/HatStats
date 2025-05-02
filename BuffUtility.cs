using ProjectM;
using Unity.Entities;
using Stunlock.Core;
using ProjectM.Network;
using ProjectM.Shared;

namespace HatStats;

public static class BuffUtility
{
    public static void TryAddBuffViaSystem(Entity user, Entity character, PrefabGUID buffGuid, int duration = 0, bool immortal = true)
    {
        var des = Core.Server.GetExistingSystemManaged<DebugEventsSystem>();
        var entityManager = Core.Server.EntityManager;

        var applyEvent = new ApplyBuffDebugEvent
        {
            BuffPrefabGUID = buffGuid
        };

        var fromCharacter = new FromCharacter
        {
            User = user,
            Character = character
        };

        des.ApplyBuff(fromCharacter, applyEvent);

        Plugin.LogInstance.LogInfo($"[BuffSystem] Buff {buffGuid} applied to entity {character.Index} via DebugEventsSystem.");
    }

    public static void TryRemoveBuffViaSystem(Entity user, Entity character, PrefabGUID buffGuid)
    {
        var entityManager = Core.Server.EntityManager;

        if (TryGetBuff(entityManager, character, buffGuid, out var buffEntity))
        {
            DestroyUtility.Destroy(entityManager, buffEntity, DestroyDebugReason.TryRemoveBuff);
            Plugin.LogInstance.LogInfo($"[BuffSystem] Removed buff {buffGuid} from entity {character.Index}");
        }
        else
        {
            Plugin.LogInstance.LogInfo($"[BuffSystem] No matching buff {buffGuid} found on entity {character.Index}");
        }
    }

    public static bool TryGetBuff(EntityManager entityManager, Entity character, PrefabGUID buffGuid, out Entity buffEntity)
    {
        var query = entityManager.CreateEntityQuery(new EntityQueryDesc
        {
            All = new ComponentType[]
            {
            ComponentType.ReadOnly<Buff>(),
            ComponentType.ReadOnly<PrefabGUID>(),
            ComponentType.ReadOnly<EntityOwner>()
            }
        });

        var entities = query.ToEntityArray(Unity.Collections.Allocator.Temp);
        try
        {
            foreach (var entity in entities)
            {
                var owner = entityManager.GetComponentData<EntityOwner>(entity);
                if (owner.Owner != character) continue;

                var guid = entityManager.GetComponentData<PrefabGUID>(entity);
                if (guid.Equals(buffGuid))
                {
                    buffEntity = entity;
                    return true;
                }
            }
        }
        finally
        {
            entities.Dispose();
        }

        buffEntity = Entity.Null;
        return false;
    }



}