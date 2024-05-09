using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System;
using UnityEngine;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        float healthPoints = -1f;

        Animator animator;
        ActionScheduler actionScheduler;
        BaseStats baseStats;

        bool isDead = false;
        public bool IsDead()
        {
            return isDead;
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
            baseStats = GetComponent<BaseStats>();
        }

        private void Start()
        {
            if(healthPoints < 0)
            {
                healthPoints = baseStats.GetStat(Stat.Health);
            }
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }


        public float GetPercentage()
        {
            return 100 * (healthPoints / baseStats.GetStat(Stat.Health));
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            animator.SetTrigger("die");
            actionScheduler.CancelCurrentAction();
        }
        private void AwardExperience(GameObject instigator)
        {
            if (!instigator.TryGetComponent<Experience>(out var experience)) return;
            experience.GainExperience(baseStats.GetStat(Stat.ExperienceReward));
        }

        public Type GetStateType()
        {
            return typeof(float);
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;
            if (healthPoints == 0)
            {
                Die();
            }
        }


    }
}