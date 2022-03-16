namespace Creatures.Ai
{
    public class AiIdleState : AiState
    {
        public override AiStateId GetId()
        {
            return AiStateId.Idle;
        }

        public override void Enter(AiAgent agent)
        {
            agent.HeroMove.StopMove();
        }

        public override void Update(AiAgent agent)
        {
        
        }

        public override void Exit(AiAgent agent)
        {
        
        }
    }
}