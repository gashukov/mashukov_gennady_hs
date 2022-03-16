using StaticData;

namespace Infrastructure.Services
{
    public interface IStaticDataService
    {
        void Load();
        LevelStaticData ForLevel();
        HeroStaticData ForHero();
    }
}