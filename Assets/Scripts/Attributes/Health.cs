using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System;
using UnityEngine;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;

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
            healthPoints = baseStats.GetHealth();
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            Debug.Log(healthPoints);

            if (healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }


        public float GetPercentage()
        {
            return 100 * (healthPoints / baseStats.GetHealth());
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
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
            experience.GainExperience(baseStats.GetExperienceReward());
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