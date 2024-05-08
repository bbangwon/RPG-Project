using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField]
        ProgressionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;


        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();

            if (!lookupTable.ContainsKey(characterClass)) return 0;
            if (!lookupTable[characterClass].ContainsKey(stat)) return 0;

            float[] levels = lookupTable[characterClass][stat];
            if(levels.Length < level) return 0;

            return levels[level - 1];
        }

        private void BuildLookup()
        {
            if(lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
            foreach (ProgressionCharacterClass progressionCharacterClass in characterClasses)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();
                foreach (ProgressionStat progressionStat in progressionCharacterClass.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }
                lookupTable[progressionCharacterClass.characterClass] = statLookupTable;
            }
        }

        [Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
        }

        [Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }
    }
}