//using HarmonyLib;
//using ProjectM;
//using ProjectM.Network;
//using Unity.Collections;
//using Unity.Entities;
//using ProjectM.Shared;
//using Stunlock.Core;
//using System.Collections.Generic;
//using ProjectM.Gameplay.Scripting;
//using ProjectM.Scripting;
//using static ProjectM.SpawnBuffsAuthoring.SpawnBuffElement_Editor;

//namespace HatStats;

//[HarmonyPatch(typeof(ReactToInventoryChangedSystem), "OnUpdate")]
//public static class HatBuffApplication
//{
//    public static Dictionary<Entity, PrefabGUID> _lastEquippedHelmet = new();

//    // Replace this with the actual PrefabGUID of your buff
//    private static readonly PrefabGUID HelmetBuff = new PrefabGUID(-1245007017);

//    public static void Prefix(ReactToInventoryChangedSystem __instance)
//    {
//        var entityManager = __instance.EntityManager;

//        var query = entityManager.CreateEntityQuery(new EntityQueryDesc
//        {
//            All = new ComponentType[] { ComponentType.ReadOnly<Equipment>() }
//        });

//        var entities = query.ToEntityArray(Allocator.Temp);
//        try
//        {
//            foreach (var entity in entities)
//            {
//                if (!entityManager.HasComponent<Equipment>(entity)) continue;

//                var equipment = entityManager.GetComponentData<Equipment>(entity);
//                var headItemEntity = equipment.ArmorHeadgearSlot.SlotEntity.GetEntityOnServer();

//                PrefabGUID currentHelmetGuid = default;
//                bool hasHelmetNow = false;

//                if (headItemEntity != Entity.Null &&
//                    entityManager.Exists(headItemEntity) &&
//                    entityManager.HasComponent<PrefabGUID>(headItemEntity))
//                {
//                    currentHelmetGuid = entityManager.GetComponentData<PrefabGUID>(headItemEntity);
//                    hasHelmetNow = currentHelmetGuid.GuidHash != 0;
//                }

//                _lastEquippedHelmet.TryGetValue(entity, out var previousHelmetGuid);
//                bool helmetChanged = !currentHelmetGuid.Equals(previousHelmetGuid);

//                if (!helmetChanged)
//                    continue;

//                //Remove / reset buff if previous helmet existed
//                if (entity.HasBuff(HelmetBuff) && previousHelmetGuid.GuidHash != 0)
//                {
//                    ProjectM.BuffUtility.TryGetBuff(Core.EntityManager, entity, HelmetBuff, out var buffBuffer);
//                    Plugin.LogInstance.LogInfo($"[HatStats] Helmet removed or changed → removing buff {HelmetBuff.GuidHash}");
//                    buffBuffer.TryDestroyBuff();

//                    FixedString512Bytes msg = $"Cleared the Buff from ({previousHelmetGuid.GuidHash})";
//                    ServerChatUtils.SendSystemMessageToAllClients(entityManager, ref msg);
//                }


//                // Equip a helm
//                if (helmetChanged)
//                {
//                    Plugin.LogInstance.LogInfo($"[HatStats] Helmet equipped or changed → applying buff {HelmetBuff.GuidHash}");


//                    if (!entity.HasBuff(HelmetBuff))
//                    {

//                        entity.TryApplyBuff(HelmetBuff);
                           
//                    }

//                    ProjectM.BuffUtility.TryGetBuff(Core.EntityManager, entity, HelmetBuff, out var buffEntity);


//                    if (buffEntity.Has<ModifyUnitStatBuff_DOTS>())
//                    {
//                        buffEntity.Remove<ModifyUnitStatBuff_DOTS>();
//                    }
                    

//                    var modifyStatBuffer = Core.EntityManager.AddBuffer<ModifyUnitStatBuff_DOTS>(buffEntity);

//                    modifyStatBuffer.Clear();

//                    HatStatConstants.HelmetBuffMap.TryGetValue(currentHelmetGuid, out List<ModifyUnitStatBuff_DOTS> buffs);

//                    foreach (var buff in buffs)
//                    {
//                        modifyStatBuffer.Add(buff);
//                    }
                        

//                    if (buffEntity.Has<LifeTime>())
//                    {
//                        var lifetime = buffEntity.Read<LifeTime>();
//                        lifetime.Duration = -1;
//                        lifetime.EndAction = LifeTimeEndAction.None;
//                        buffEntity.Write(lifetime);
//                    }

//                    if (buffEntity.Has<RemoveBuffOnGameplayEvent>())
//                    {
//                        buffEntity.Remove<RemoveBuffOnGameplayEvent>();
//                    }
//                    if (buffEntity.Has<RemoveBuffOnGameplayEventEntry>())
//                    {
//                        buffEntity.Remove<RemoveBuffOnGameplayEventEntry>();
//                    }
//                    if (buffEntity.Has<Script_Modify_Combat_Movement_Buff_Data>())
//                    {
//                        buffEntity.Remove<Script_Modify_Combat_Movement_Buff_Data>();
//                    }
//                    if (buffEntity.Has<Script_Modify_Combat_Movement_Buff_State>())
//                    {
//                        buffEntity.Remove<Script_Modify_Combat_Movement_Buff_State>();
//                    }
//                    if (buffEntity.Has<DestroyOnGameplayEvent>())
//                    {
//                        buffEntity.Remove<DestroyOnGameplayEvent>();
//                    }
//                    if (buffEntity.Has<CreateGameplayEventsOnTick>())
//                    {
//                        buffEntity.Remove<CreateGameplayEventsOnTick>();
//                    }
//                    if (buffEntity.Has<ScriptSpawn>())
//                    {
//                        buffEntity.Remove<ScriptSpawn>();
//                    }



//                    FixedString512Bytes msg = $"Equipped hat (GUID: {currentHelmetGuid.GuidHash})";
//                    ServerChatUtils.SendSystemMessageToAllClients(entityManager, ref msg);
//                }

//                // Track for next update
//                _lastEquippedHelmet[entity] = currentHelmetGuid;
                
//            }
//        }
//        finally
//        {
//            entities.Dispose();
//        }
//    }
//}
