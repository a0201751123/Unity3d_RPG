using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "RPG Project/Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;
        public float GetHealth(CharacterClass characterClass,int level)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if(progressionClass.characterClass == characterClass)
                {
                    return progressionClass.health[level - 1];
                }
            }
            return 0;
        }
        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public float[] health;


        }
    }
}