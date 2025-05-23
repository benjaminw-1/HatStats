using HarmonyLib;
using ProjectM;
using ProjectM.Network;
using Stunlock.Network;
using Stunlock.Core;
using Unity.Entities;
using System;

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
            
            if (userData.CharacterName.IsEmpty) return;

            var charEntity = userData.LocalCharacter.GetEntityOnServer();

            if (charEntity == Entity.Null || !em.Exists(charEntity) || !em.HasComponent<Equipment>(charEntity))
                return;

            var equipment = em.GetComponentData<Equipment>(charEntity);
            var helmetEntity = equipment.ArmorHeadgearSlot.SlotEntity.GetEntityOnServer();

            if (helmetEntity == Entity.Null ||
                !em.Exists(helmetEntity) ||
                !em.HasComponent<PrefabGUID>(helmetEntity))
                return;

            var helmetGuid = em.GetComponentData<PrefabGUID>(helmetEntity);

            if (HatStatConstants.HelmetBuffMap.TryGetValue(helmetGuid, out var buffGuid))
            {
                //BuffUtility.TryRemoveBuffViaSystem(userEntity, charEntity, buffGuid);

                BuffUtility.TryAddBuffViaSystem(userEntity, charEntity, buffGuid);
                ReactToInventory._lastEquippedHelmet[charEntity] = helmetGuid;

                Plugin.LogInstance.LogInfo($"[HatStats] On login: refreshed helmet buff {buffGuid.GuidHash} for character {charEntity.Index}");
            }
        }
        catch (Exception e)
        {
            Plugin.LogInstance.LogError($"[HatStats] Error in OnUserConnected_Patch: {e.Message}\n{e.StackTrace}");
        }
    }
}
