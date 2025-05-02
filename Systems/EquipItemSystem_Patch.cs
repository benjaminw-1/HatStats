//using HarmonyLib;
//using ProjectM;
//using ProjectM.Network;
//using Unity.Collections;
//using Unity.Entities;
//using Stunlock.Core;

//namespace HatStats;

//[HarmonyPatch(typeof(EquipItemSystem), "OnUpdate")]
//public static class EquipItemSystem_Patch
//{
//	public static void Prefix(EquipItemSystem __instance)
//	{
//		var entityManager = __instance.EntityManager;
//		var query = __instance._EventQuery;

//		var entities = query.ToEntityArray(Allocator.Temp);
//		try
//		{
//			foreach (var entity in entities)
//			{
//				if (!entityManager.HasComponent<EquipItemEvent>(entity)) continue;
//				if (!entityManager.HasComponent<FromCharacter>(entity)) continue;

//				var fromCharacter = entityManager.GetComponentData<FromCharacter>(entity);

//				if (!entityManager.Exists(fromCharacter.Character)) continue;

//				// TEMP: Always apply the helmet buff on any EquipItemEvent
//				Plugin.LogInstance.LogInfo($"[HatStats] EquipItemEvent on {fromCharacter.Character.Index}");
//				ServerChatUtils.SendSystemMessageToAllClients(Core.EntityManager, $"I HAVE RIGHTCLICKED MY HAT");
//				BuffUtility.TryAddBuffViaSystem(fromCharacter.User, fromCharacter.Character, HatStatConstants.HelmetStatBuffGUID);

//			}
//		}
//		finally
//		{
//			entities.Dispose();
//		}
//	}
//}
