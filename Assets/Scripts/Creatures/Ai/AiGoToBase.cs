using UnityEngine;

namespace Creatures.Ai
{
    public class AiGoToBase : AiTargetWaypointState
    {
        public override AiStateId GetId()
        {
            return AiStateId.GoToBase;
        }

        public override void Enter(AiAgent agent)
        {
            if (agent.IsOnBase())
            {
                agent.Do(AiStateId.Idle);
                return;
            }
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
                agent.Do(AiStateId.Idle);
            }
            
        }

        protected override void ChangeTarget(AiAgent agent)
        {
            Target = agent.HeroData.BaseWaypoint;
        }
    }
}