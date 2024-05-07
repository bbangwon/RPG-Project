using RPG.Saving;
using System;
using UnityEngine;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;



        public void GainExperience(float experience)
        {
            experiencePoints += experience;
        }

        public Type GetStateType()
        {
            return typeof(float);
        }

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }

}