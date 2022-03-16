using Helpers;
using UnityEngine;

namespace Creatures.Ai
{
    public class AiPatrolState : AiTargetWaypointState
    {

        public override AiStateId GetId()
        {
            return AiStateId.Patrol;
        }

        public override void Enter(AiAgent agent)
        {
            ChangeTarget(agent);
            agent.OnTriggerWaypoint += collider => OnTriggerWaypoint(agent, collider);
        
        }

        public override void Update(AiAgent agent)
        {
        
            agent.HeroMove.MoveTo(Target.position, agent.HeroData.Speed);

        }

        public override void Exit(AiAgent agent)
        {
            agent.OnTriggerWaypoint = null;
            agent.HeroMove.StopMove();
        }

        protected override void OnTriggerWaypoint(AiAgent agent, Collider collider)
        {
            if (collider.transform.Equals(Target))
            {
                ChangeTarget(agent);
            }
        }

        protected override void ChangeTarget(AiAgent agent)
        {
            Target = agent.HeroData.Waypoints.PickRandomOther(Target);
        }
    }
}