using ProjectM;
using Unity.Entities;
using Stunlock.Core;
using ProjectM.Network;

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
}