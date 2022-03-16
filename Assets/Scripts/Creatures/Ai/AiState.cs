namespace Creatures.Ai
{
    public abstract class AiState
    {
        public abstract AiStateId GetId();
        public abstract void Enter(AiAgent agent);
        public abstract void Update(AiAgent agent);
        public abstract void Exit(AiAgent agent);
    
    }
}