using Il2CppInterop.Runtime;
using ProjectM;
using ProjectM.Gameplay.Systems;
using ProjectM.Network;
using ProjectM.Scripting;
using ProjectM.Shared;
using Stunlock.Core;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;


namespace HatStats;
internal static class Extensions // probably need to organize this soonTM and at least separate out Bloodcraft specific ones so easier to copy and paste around
{
    static EntityManager EntityManager => Core.EntityManager;
    static ServerGameManager ServerGameManager => Core.ServerGameManager;
    static DebugEventsSystem DebugEventsSystem => Core.DebugEventsSystem;
    static PrefabCollectionSystem PrefabCollectionSystem => Core.PrefabCollectionSystem;



    public struct PlayerInfo(Entity userEntity = default, Entity charEntity = default, User user = default)
    {
        public User User { get; set; } = user;
        public Entity UserEntity { get; set; } = userEntity;
        public Entity CharEntity { get; set; } = charEntity;
    }

    static readonly ConcurrentDictionary<ulong, PlayerInfo> _steamIdPlayerInfoCache = [];
    public static IReadOnlyDictionary<ulong, PlayerInfo> SteamIdPlayerInfoCache => _steamIdPlayerInfoCache;

    static readonly ConcurrentDictionary<ulong, PlayerInfo> _steamIdOnlinePlayerInfoCache = [];
    public static IReadOnlyDictionary<ulong, PlayerInfo> SteamIdOnlinePlayerInfoCache => _steamIdOnlinePlayerInfoCache;

    public delegate void WithRefHandler<T>(ref T item);
    public static void With<T>(this Entity entity, WithRefHandler<T> action) where T : struct
    {
        T item = entity.ReadRW<T>();
        action(ref item);

        EntityManager.SetComponentData(entity, item);
    }
    public static void AddWith<T>(this Entity entity, WithRefHandler<T> action) where T : struct
    {
        if (!entity.Has<T>())
        {
            entity.Add<T>();
        }

        entity.With(action);
    }
    public static void HasWith<T>(this Entity entity, WithRefHandler<T> action) where T : struct
    {
        if (entity.Has<T>())
        {
            entity.With(action);
        }
    }
    public unsafe static void Write<T>(this Entity entity, T componentData) where T : struct
    {
        ComponentType componentType = new(Il2CppType.Of<T>());
        TypeIndex typeIndex = componentType.TypeIndex;

        byte[] byteArray = StructureToByteArray(componentData);
        int size = Marshal.SizeOf<T>();

        fixed (byte* byteData = byteArray)
        {
            EntityManager.SetComponentDataRaw(entity, typeIndex, byteData, size);
        }
    }
    static byte[] StructureToByteArray<T>(T structure) where T : struct
    {
        int size = Marshal.SizeOf(structure);
        byte[] byteArray = new byte[size];

        IntPtr ptr = Marshal.AllocHGlobal(size);
        Marshal.StructureToPtr(structure, ptr, true);

        Marshal.Copy(ptr, byteArray, 0, size);
        Marshal.FreeHGlobal(ptr);

        return byteArray;
    }
    unsafe static T ReadRW<T>(this Entity entity) where T : struct
    {
        ComponentType componentType = new(Il2CppType.Of<T>());
        TypeIndex typeIndex = componentType.TypeIndex;

        void* componentData = EntityManager.GetComponentDataRawRW(entity, typeIndex);
        return Marshal.PtrToStructure<T>(new IntPtr(componentData));
    }
    public unsafe static T Read<T>(this Entity entity) where T : struct
    {
        ComponentType componentType = new(Il2CppType.Of<T>());
        TypeIndex typeIndex = componentType.TypeIndex;

        void* componentData = EntityManager.GetComponentDataRawRO(entity, typeIndex);
        return Marshal.PtrToStructure<T>(new IntPtr(componentData));
    }
    public static DynamicBuffer<T> ReadBuffer<T>(this Entity entity) where T : struct
    {
        return EntityManager.GetBuffer<T>(entity);
    }
    public static DynamicBuffer<T> AddBuffer<T>(this Entity entity) where T : struct
    {
        return EntityManager.AddBuffer<T>(entity);
    }
    public static bool TryGetComponent<T>(this Entity entity, out T componentData) where T : struct
    {
        componentData = default;

        if (entity.Has<T>())
        {
            componentData = entity.Read<T>();
            return true;
        }

        return false;
    }
    public static bool TryRemove<T>(this Entity entity) where T : struct
    {
        if (entity.Has<T>())
        {
            entity.Remove<T>();

            return true;
        }

        return false;
    }
    public static bool Has<T>(this Entity entity)
    {
        return EntityManager.HasComponent(entity, new(Il2CppType.Of<T>()));
    }
    public static bool Has(this Entity entity, ComponentType componentType)
    {
        return EntityManager.HasComponent(entity, componentType);
    }
    public static string GetPrefabName(this PrefabGUID prefabGUID)
    {
        return PrefabCollectionSystem.PrefabGuidToNameDictionary.TryGetValue(prefabGUID, out string prefabName) ? $"{prefabName} {prefabGUID}" : "String.Empty";
    }
    public static void Add<T>(this Entity entity)
    {
        EntityManager.AddComponent(entity, new(Il2CppType.Of<T>()));
    }
    public static void Remove<T>(this Entity entity)
    {
        EntityManager.RemoveComponent(entity, new(Il2CppType.Of<T>()));
    }
    public static bool TryGetFollowedPlayer(this Entity entity, out Entity player)
    {
        player = Entity.Null;

        if (entity.TryGetComponent(out Follower follower))
        {
            if (follower.Followed._Value.TryGetPlayer(out player))
            {
                return true;
            }
        }

        return false;
    }
    public static bool TryGetPlayer(this Entity entity, out Entity player)
    {
        player = Entity.Null;

        if (entity.Has<PlayerCharacter>())
        {
            player = entity;

            return true;
        }

        return false;
    }
    public static bool IsPlayer(this Entity entity)
    {
        if (entity.Has<PlayerCharacter>())
        {
            return true;
        }

        return false;
    }
    public static bool IsFollowingPlayer(this Entity entity)
    {
        if (entity.Has<BlockFeedBuff>() && !entity.Has<Buff>() && !entity.Has<ServantEquipment>())
        {
            return true;
        }
        else if (entity.TryGetComponent(out Follower follower))
        {
            if (follower.Followed._Value.IsPlayer())
            {
                return true;
            }
        }

        return false;
    }
    public static bool TryGetAttached(this Entity entity, out Entity attached)
    {
        attached = Entity.Null;

        if (entity.TryGetComponent(out Attach attach) && attach.Parent.Exists())
        {
            attached = attach.Parent;
            return true;
        }

        return false;
    }
    public static Entity GetBuffTarget(this Entity entity)
    {
        return CreateGameplayEventServerUtility.GetBuffTarget(EntityManager, entity);
    }
    public static Entity GetPrefabEntity(this Entity entity)
    {
        return ServerGameManager.GetPrefabEntity(entity.Read<PrefabGUID>());
    }
    public static Entity GetPrefabEntity(this PrefabGUID prefabGuid)
    {
        return ServerGameManager.GetPrefabEntity(prefabGuid);
    }
    public static Entity GetSpellTarget(this Entity entity)
    {
        return CreateGameplayEventServerUtility.GetSpellTarget(EntityManager, entity);
    }
    public static bool TryGetTeamEntity(this Entity entity, out Entity teamEntity)
    {
        teamEntity = Entity.Null;

        if (entity.TryGetComponent(out TeamReference teamReference))
        {
            Entity teamReferenceEntity = teamReference.Value._Value;

            if (teamReferenceEntity.Exists())
            {
                teamEntity = teamReferenceEntity;
                return true;
            }
        }

        return false;
    }
    public static bool Exists(this Entity entity)
    {
        return entity.HasValue() && EntityManager.Exists(entity);
    }
    public static bool HasValue(this Entity entity)
    {
        return entity != Entity.Null;
    }
    public static bool IsDisabled(this Entity entity)
    {
        return entity.Has<Disabled>();
    }
    public static bool IsVBlood(this Entity entity)
    {
        return entity.Has<VBloodConsumeSource>();
    }
    public static bool IsGateBoss(this Entity entity)
    {
        return entity.Has<VBloodUnit>() && !entity.Has<VBloodConsumeSource>();
    }
    public static bool IsVBloodOrGateBoss(this Entity entity)
    {
        return entity.Has<VBloodUnit>();
    }
    public static bool IsLegendary(this Entity entity)
    {
        return entity.Has<LegendaryItemInstance>();
    }
    public static bool IsAncestralWeapon(this Entity entity)
    {
        return entity.Has<LegendaryItemInstance>() && !entity.IsMagicSource();
    }
    public static bool IsShardNecklace(this Entity entity)
    {
        return entity.Has<LegendaryItemInstance>() && entity.IsMagicSource();
    }
    public static bool IsMagicSource(this Entity entity)
    {
        return entity.TryGetComponent(out EquippableData equippableData) && equippableData.EquipmentType.Equals(EquipmentType.MagicSource);
    }
    public static ulong GetSteamId(this Entity entity)
    {
        if (entity.TryGetComponent(out PlayerCharacter playerCharacter))
        {
            return playerCharacter.UserEntity.GetUser().PlatformId;
        }
        else if (entity.TryGetComponent(out User user))
        {
            return user.PlatformId;
        }

        return 0; // maybe this should be -1 instead since steamId 0 sneaks in to weird places sometimes? noting for later
    }
    public static NetworkId GetNetworkId(this Entity entity)
    {
        if (entity.TryGetComponent(out NetworkId networkId))
        {
            return networkId;
        }

        return NetworkId.Empty;
    }
    public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
    {
        foreach (var item in collection)
        {
            action(item);
        }
    }
    public static bool TryGetPlayerInfo(this ulong steamId, out PlayerInfo playerInfo)
    {
        if (SteamIdPlayerInfoCache.TryGetValue(steamId, out playerInfo)) return true;
        else if (SteamIdOnlinePlayerInfoCache.TryGetValue(steamId, out playerInfo)) return true;

        return false;
    }
    public static PrefabGUID GetPrefabGuid(this Entity entity)
    {
        if (entity.TryGetComponent(out PrefabGUID prefabGuid)) return prefabGuid;

        return PrefabGUID.Empty;
    }
    public static int GetGuidHash(this Entity entity)
    {
        if (entity.TryGetComponent(out PrefabGUID prefabGUID)) return prefabGUID.GuidHash;

        return PrefabGUID.Empty.GuidHash;
    }
    public static Entity GetUserEntity(this Entity entity)
    {
        if (entity.TryGetComponent(out PlayerCharacter playerCharacter)) return playerCharacter.UserEntity;
        else if (entity.Has<User>()) return entity;

        return Entity.Null;
    }
    public static Entity GetOwner(this Entity entity)
    {
        if (entity.TryGetComponent(out EntityOwner entityOwner) && entityOwner.Owner.Exists()) return entityOwner.Owner;

        return Entity.Null;
    }
    public static User GetUser(this Entity entity)
    {
        if (entity.TryGetComponent(out User user)) return user;
        else if (entity.TryGetComponent(out PlayerCharacter playerCharacter) && playerCharacter.UserEntity.TryGetComponent(out user)) return user;

        return User.Empty;
    }
    public static bool HasBuff(this Entity entity, PrefabGUID buffPrefabGuid)
    {
        return ServerGameManager.HasBuff(entity, buffPrefabGuid.ToIdentifier());
    }
    public static unsafe bool TryGetBuffer<T>(this Entity entity, out DynamicBuffer<T> dynamicBuffer) where T : struct
    {
        if (ServerGameManager.TryGetBuffer(entity, out dynamicBuffer))
        {
            return true;
        }

        return false;
    }
    public static float3 GetAimPosition(this Entity entity)
    {
        if (entity.TryGetComponent(out EntityInput entityInput))
        {
            return entityInput.AimPosition;
        }

        return float3.zero;
    }
    public static float3 GetPosition(this Entity entity)
    {
        if (entity.TryGetComponent(out Translation translation))
        {
            return translation.Value;
        }

        return float3.zero;
    }
    public static int2 GetTileCoord(this Entity entity)
    {
        if (entity.TryGetComponent(out TilePosition tilePosition))
        {
            return tilePosition.Tile;
        }

        return int2.zero;
    }
    public static int GetUnitLevel(this Entity entity)
    {
        if (entity.TryGetComponent(out UnitLevel unitLevel))
        {
            return unitLevel.Level._Value;
        }

        return 0;
    }
    public static float GetMaxDurability(this Entity entity)
    {
        if (entity.TryGetComponent(out Durability maxDurability))
        {
            return maxDurability.Value;
        }

        return 0;
    }
    public static float GetMaxHealth(this Entity entity)
    {
        if (entity.TryGetComponent(out Health health))
        {
            return health.MaxHealth._Value;
        }

        return 0;
    }
    public static Blood GetBlood(this Entity entity)
    {
        if (entity.TryGetComponent(out Blood blood))
        {
            return blood;
        }

        throw new InvalidOperationException("Entity does not have Blood!");
    }
    public static (float physicalPower, float spellPower) GetPowerTuple(this Entity entity)
    {
        if (entity.TryGetComponent(out UnitStats unitStats))
        {
            return (unitStats.PhysicalPower._Value, unitStats.SpellPower._Value);
        }

        return (0f, 0f);
    }
    public static bool IsUnitSpawnerSpawned(this Entity entity)
    {
        if (entity.TryGetComponent(out IsMinion isMinion) && isMinion.Value)
        {
            return true;
        }

        return false;
    }
    public static void TryDestroy(this Entity entity)
    {
        if (entity.Exists()) DestroyUtility.Destroy(EntityManager, entity);
    }
    public static void TryDestroyBuff(this Entity buffEntity)
    {
        if (buffEntity.Exists()) DestroyUtility.Destroy(EntityManager, buffEntity, DestroyDebugReason.TryRemoveBuff);
    }
    public static void SetTeam(this Entity entity, Entity teamSource)
    {
        if (entity.Has<Team>() && entity.Has<TeamReference>() && teamSource.TryGetComponent(out Team sourceTeam) && teamSource.TryGetComponent(out TeamReference sourceTeamReference))
        {
            Entity teamRefEntity = sourceTeamReference.Value._Value;
            int teamId = sourceTeam.Value;

            entity.With((ref TeamReference teamReference) =>
            {
                teamReference.Value._Value = teamRefEntity;
            });

            entity.With((ref Team team) =>
            {
                team.Value = teamId;
            });
        }
    }
    public static void SetPosition(this Entity entity, float3 position)
    {
        if (entity.Has<Translation>())
        {
            entity.With((ref Translation translation) =>
            {
                translation.Value = position;
            });
        }

        if (entity.Has<LastTranslation>())
        {
            entity.With((ref LastTranslation lastTranslation) =>
            {
                lastTranslation.Value = position;
            });
        }
    }
    public static void SetFaction(this Entity entity, PrefabGUID factionPrefabGuid)
    {
        if (entity.Has<FactionReference>())
        {
            entity.With((ref FactionReference factionReference) =>
            {
                factionReference.FactionGuid._Value = factionPrefabGuid;
            });
        }
    }
    public static bool IsAllies(this Entity entity, Entity player)
    {
        return ServerGameManager.IsAllies(entity, player);
    }
    public static bool IsPlayerOwned(this Entity entity)
    {
        if (entity.TryGetComponent(out EntityOwner entityOwner))
        {
            return entityOwner.Owner.IsPlayer();
        }

        return false;
    }
    public static void CastAbility(this Entity entity, Entity target, PrefabGUID abilityGroup)
    {
        bool isPlayer = entity.IsPlayer();

        CastAbilityServerDebugEvent castAbilityServerDebugEvent = new()
        {
            AbilityGroup = abilityGroup,
            Who = target.GetNetworkId()
        };

        FromCharacter fromCharacter = new()
        {
            Character = entity,
            User = isPlayer ? entity.GetUserEntity() : entity
        };

        int userIndex = isPlayer ? entity.GetUser().Index : 0;
        DebugEventsSystem.CastAbilityServerDebugEvent(userIndex, ref castAbilityServerDebugEvent, ref fromCharacter);
    }
   
    public static IEnumerator WaitForCompletion(this JobHandle handle)
    {
        return WaitForCompletionRoutine(handle);
    }
    static IEnumerator WaitForCompletionRoutine(JobHandle handle)
    {
        while (!handle.IsCompleted)
        {
            yield return null;
        }
    }
    public static void LogComponentTypes(this Entity entity)
    {
        NativeArray<ComponentType>.Enumerator enumerator = EntityManager.GetComponentTypes(entity).GetEnumerator();
        Core.Log.LogInfo("===");
        while (enumerator.MoveNext())
        {
            ComponentType current = enumerator.Current;
            Core.Log.LogInfo($"{current}");
        }
        Core.Log.LogInfo("===");
    }
    public static bool TryApplyBuff(this Entity entity, PrefabGUID buffPrefabGuid)
    {
        if (!entity.HasBuff(buffPrefabGuid))
        {
            ApplyBuffDebugEvent applyBuffDebugEvent = new()
            {
                BuffPrefabGUID = buffPrefabGuid
            };

            FromCharacter fromCharacter = new()
            {
                Character = entity,
                User = entity
            };

            Core.DebugEventsSystem.ApplyBuff(fromCharacter, applyBuffDebugEvent);
            return true;
        }

        return false;
    }
}

/*
// note to organize this into ECBExtensions or something if I actually need to use it for familiars or anything else at some point
public static void SetTeam(this Entity entity, EntityCommandBuffer entityCommandBuffer, Entity teamSource)
{
    if (teamSource.TryGetComponent(out Team sourceTeam) && teamSource.TryGetComponent(out TeamReference sourceTeamReference))
    {
        Entity teamRefEntity = sourceTeamReference.Value._Value;
        int teamId = sourceTeam.Value;

        entityCommandBuffer.SetComponent(entity, new TeamReference { Value = new ModifiableEntity { _Value = teamRefEntity } });
        entityCommandBuffer.SetComponent(entity, new Team { Value = teamId });
    }
}
public static void SetPosition(this Entity entity, EntityCommandBuffer entityCommandBuffer, float3 position)
{
    entityCommandBuffer.SetComponent(entity, new Translation { Value = position });
    entityCommandBuffer.SetComponent(entity, new LastTranslation { Value = position });
}
public static void SetFaction(this Entity entity, EntityCommandBuffer entityCommandBuffer, PrefabGUID factionPrefabGUID)
{
    entityCommandBuffer.SetComponent(entity, new FactionReference { FactionGuid = new ModifiablePrefabGUID { _Value = factionPrefabGUID } });
}
*/