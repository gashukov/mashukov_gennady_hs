using System.Threading.Tasks;
using Infrastructure.States;

namespace Infrastructure.Services
{
    public interface IGameFactory
    {
        Task CreateWaypoints();
        Task CreateHero();
        Task CreateHud(IGameStateMachine gameStateMachine);
        Task CreateClickRaycaster();
        Task WarmUp();
        void Cleanup();
    }
}