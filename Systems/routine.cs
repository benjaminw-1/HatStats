using ProjectM.CastleBuilding;
using ProjectM;
using ProjectM.Physics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;
using static ProjectM.Sequencer.FullscreenEffectSystem;
using BepInEx.Unity.IL2CPP.Utils.Collections;
using Unity.Collections;
using ProjectM.Network;
using Il2CppInterop.Runtime;
using UnityEngine.TextCore.Text;
using Stunlock.Core;
using ProjectM.Gameplay.Scripting;
using ProjectM.Scripting;



namespace HatStats.Systems
{

    internal class routine
    {
        static MonoBehaviour monoBehaviour;
        public static bool active = false;
        private static readonly PrefabGUID HelmetBuff = new PrefabGUID(-1245007017);
        public static Dictionary<Entity, PrefabGUID> _lastEquippedHelmet = new();
        public static void StartCoroutine(IEnumerator routine)
        {
            if (monoBehaviour == null)
            {
                var go = new GameObject("test");
                monoBehaviour = go.AddComponent<IgnorePhysicsDebugSystem>();
                UnityEngine.Object.DontDestroyOnLoad(go);
            }
            monoBehaviour.StartCoroutine(routine.WrapToIl2Cpp());
        }
        public static  IEnumerator test()
        {
            active = true;
            yield return null;
            while (true)
            {
                var players = GetEntitiesByComponentType<PlayerCharacter>();


                foreach (Entity player in players) 
                {
                    var equipment = player.Read<Equipment>();
                    var headItemEntity = equipment.ArmorHeadgearSlot.SlotEntity.GetEntityOnServer();


                    PrefabGUID currentHelmetGuid = default;


                    //check if player has a hat equipped, if not remove the hat buff
                    if (headItemEntity != Entity.Null &&
                        Core.EntityManager.Exists(headItemEntity) &&
                        Core.EntityManager.HasComponent<PrefabGUID>(headItemEntity))
                    {
                        currentHelmetGuid = Core.EntityManager.GetComponentData<PrefabGUID>(headItemEntity);
                    }
                    else 
                    {
                        ProjectM.BuffUtility.TryGetBuff(Core.EntityManager, player, HelmetBuff, out var toKill);
                        toKill.TryDestroyBuff();
                    }

                        _lastEquippedHelmet.TryGetValue(player, out var previousHelmetGuid);
                    bool helmetChanged = !currentHelmetGuid.Equals(previousHelmetGuid);



                    //if the player has a new hat but his buff is still valid destroy the buff, dont tell it it equipped the new hat
                    if (helmetChanged && ProjectM.BuffUtility.TryGetBuff(Core.EntityManager, player, HelmetBuff, out var buffEntity))
                    {
                        buffEntity.TryDestroyBuff();
                        continue;
                    }
                    //if the player doesnt have the buff but a new hat its time to reapply the buff cleanly
                    else if (helmetChanged)
                    {
                        player.TryApplyBuff(HelmetBuff);
                        cleanupBuff(player);
                        ProjectM.BuffUtility.TryGetBuff(Core.EntityManager, player, HelmetBuff, out var newBuff);

                        var modifyStatBuffer = Core.EntityManager.AddBuffer<ModifyUnitStatBuff_DOTS>(newBuff);

                        modifyStatBuffer.Clear();

                        HatStatConstants.HelmetBuffMap.TryGetValue(currentHelmetGuid, out List<ModifyUnitStatBuff_DOTS> buffs);

                        if (buffs != null)
                        {
                            foreach (var buff in buffs)
                            {
                                modifyStatBuffer.Add(buff);
                            }
                        }

                    }

                    //update the hat 
                    _lastEquippedHelmet[player] = currentHelmetGuid;

                }

               
                yield return null;
            }
        }


        public static NativeArray<Entity> GetEntitiesByComponentType<T1>(bool includeAll = false, bool includeDisabled = false, bool includeSpawn = false, bool includePrefab = false, bool includeDestroyed = false)
        {
            EntityQueryOptions options = EntityQueryOptions.Default;
            if (includeAll) options |= EntityQueryOptions.IncludeAll;
            if (includeDisabled) options |= EntityQueryOptions.IncludeDisabled;
            if (includeSpawn) options |= EntityQueryOptions.IncludeSpawnTag;
            if (includePrefab) options |= EntityQueryOptions.IncludePrefab;
            if (includeDestroyed) options |= EntityQueryOptions.IncludeDestroyTag;

            EntityQueryDesc queryDesc = new()
            {
                All = new ComponentType[] { new(Il2CppType.Of<T1>(), ComponentType.AccessMode.ReadWrite) },
                Options = options
            };

            var query = Core.EntityManager.CreateEntityQuery(queryDesc);

            var entities = query.ToEntityArray(Allocator.Temp);
            return entities;
        }

        static void cleanupBuff(Entity entity)
        {

            if (!ProjectM.BuffUtility.TryGetBuff(Core.EntityManager, entity, HelmetBuff, out var buffEntity)) return;

            if (buffEntity.Has<LifeTime>())
            {
                var lifetime = buffEntity.Read<LifeTime>();
                lifetime.Duration = -1;
                lifetime.EndAction = LifeTimeEndAction.None;
                buffEntity.Write(lifetime);
            }
            if (buffEntity.Has<Buff>())
            {
                var buff = buffEntity.Read<Buff>();
                buff.BuffType = BuffType.Block;
                buff.OneInstancePerOwner = true;
                buffEntity.Write(buff);
            }

            if (buffEntity.Has<RemoveBuffOnGameplayEvent>())
            {
                buffEntity.Remove<RemoveBuffOnGameplayEvent>();
            }
            if (buffEntity.Has<RemoveBuffOnGameplayEventEntry>())
            {
                buffEntity.Remove<RemoveBuffOnGameplayEventEntry>();
            }
            if (buffEntity.Has<Script_Modify_Combat_Movement_Buff_Data>())
            {
                buffEntity.Remove<Script_Modify_Combat_Movement_Buff_Data>();
            }
            if (buffEntity.Has<Script_Modify_Combat_Movement_Buff_State>())
            {
                buffEntity.Remove<Script_Modify_Combat_Movement_Buff_State>();
            }
            if (buffEntity.Has<DestroyOnGameplayEvent>())
            {
                buffEntity.Remove<DestroyOnGameplayEvent>();
            }
            if (buffEntity.Has<CreateGameplayEventsOnTick>())
            {
                buffEntity.Remove<CreateGameplayEventsOnTick>();
            }
            if (buffEntity.Has<ScriptSpawn>())
            {
                buffEntity.Remove<ScriptSpawn>();
            }

            if (buffEntity.Has<ModifyUnitStatBuff_DOTS>())
            {
                buffEntity.Remove<ModifyUnitStatBuff_DOTS>();
            }
        }
    }
}
