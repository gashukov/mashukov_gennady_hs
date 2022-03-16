using StaticData;
using UnityEngine;

namespace Infrastructure.Services
{
    public class StaticDataService : IStaticDataService
    {
        private const string LevelDataPath = "Static Data/LevelData";
        private const string HeroDataPath = "Static Data/HeroData";
        private LevelStaticData _levelData;
        private HeroStaticData _heroData;

        public void Load()
        {
            _levelData = Resources
                .Load<LevelStaticData>(LevelDataPath);
            _heroData = Resources
                .Load<HeroStaticData>(HeroDataPath);
        }


        public LevelStaticData ForLevel() => _levelData;
        public HeroStaticData ForHero() => _heroData;

    }
}