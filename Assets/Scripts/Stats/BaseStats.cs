using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        Experience experience;
        int currentLevel = 0;

        private void Awake()
        {
            experience = GetComponent<Experience>();
        }

        private void Start()
        {
            currentLevel = CalculateLevel();
        }

        private void Update()
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel)
            {
                currentLevel = newLevel;
                print("Levelled up!");
            }
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, CalculateLevel());
        }

        public int GetLevel()
        {
            return currentLevel;
        }

        public int CalculateLevel()
        {
            if (experience == null) 
                return startingLevel;

            float currentXP = experience.GetPoints();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }
            return penultimateLevel + 1;

        }
    }
}