using UnityEngine;

namespace Creatures.Ai
{
    public abstract class AiTargetWaypointState : AiState
    {
        protected Transform Target;
        protected abstract void OnTriggerWaypoint(AiAgent agent, Collider collider);
        protected abstract void ChangeTarget(AiAgent agent);
    }
}