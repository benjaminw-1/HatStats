using HarmonyLib;
using ProjectM;
using ProjectM.Network;
using Stunlock.Network;
using Stunlock.Core;
using Unity.Entities;
using System;
using Unity.Collections;
using HatStats.Systems;

namespace HatStats;

[HarmonyPatch(typeof(ServerBootstrapSystem), nameof(ServerBootstrapSystem.OnUserConnected))]
public static class OnUserConnected_Patch
{
    public static void Postfix(ServerBootstrapSystem __instance, NetConnectionId netConnectionId)
    {
        try
        {
            var em = __instance.EntityManager;

            var userIndex = __instance._NetEndPointToApprovedUserIndex[netConnectionId];
            var serverClient = __instance._ApprovedUsersLookup[userIndex];
            var userEntity = serverClient.UserEntity;

            if (!em.Exists(userEntity) || !em.HasComponent<User>(userEntity))
                return;

            var userData = em.GetComponentData<User>(userEntity);

            //if (userData.CharacterName.IsEmpty) return;

            //var charEntity = userData.LocalCharacter.GetEntityOnServer();

            //if (charEntity == Entity.Null || !em.Exists(charEntity) || !em.HasComponent<Equipment>(charEntity))
            //    return;

            //var equipment = em.GetComponentData<Equipment>(charEntity);
            //var helmetEntity = equipment.ArmorHeadgearSlot.SlotEntity.GetEntityOnServer();

            //if (helmetEntity == Entity.Null ||
            //    !em.Exists(helmetEntity) ||
            //    !em.HasComponent<PrefabGUID>(helmetEntity))
            //    return;

            //var helmetGuid = em.GetComponentData<PrefabGUID>(helmetEntity);

            //if (HatStatConstants.HelmetBuffMap.TryGetValue(helmetGuid, out var buffGuid))
            //{

            //    HatBuffApplication._lastEquippedHelmet[charEntity] = helmetGuid;

            //    
            //}
            if (!routine.active)
            {
                routine.StartCoroutine(routine.test());
            }
            FixedString512Bytes msg = $"User logged in: Routine started)";
            ServerChatUtils.SendSystemMessageToAllClients(em, ref msg);
        }
        catch (Exception e)
        {
            Plugin.LogInstance.LogError($"[HatStats] Error in OnUserConnected_Patch: {e.Message}\n{e.StackTrace}");
        }
    }
}
