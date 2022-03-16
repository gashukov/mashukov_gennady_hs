namespace Creatures.Ai
{
    public class AiDeathState : AiState
    {
        public override AiStateId GetId()
        {
            return AiStateId.Death;
        }

        public override void Enter(AiAgent agent)
        {
            agent.Animator.SetBool("Dead", true);
            agent.HeroHealth.OnDamage = null;
        }

        public override void Update(AiAgent agent)
        {
        }

        public override void Exit(AiAgent agent)
        {
        }
    }
}