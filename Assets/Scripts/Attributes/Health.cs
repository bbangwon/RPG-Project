using GameDevTV.Utils;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70f;
        [SerializeField] UnityEvent<float> takeDamage;

        LazyValue<float> healthPoints;

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
            InitComponents();
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private void Start()
        {
            healthPoints.ForceInit();
        }

        private void OnEnable()
        {
            baseStats.onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            baseStats.onLevelUp -= RegenerateHealth;
        }

        private float GetInitialHealth()
        {
            return baseStats.GetStat(Stat.Health);
        }

        private void InitComponents()
        {
            if(animator == null)
                animator = GetComponent<Animator>();
            if (actionScheduler == null)
                actionScheduler = GetComponent<ActionScheduler>();
            if (baseStats == null)
                baseStats = GetComponent<BaseStats>();            
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            Debug.Log(gameObject.name + " took damage: " + damage);

            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);

            if (healthPoints.value == 0)
            {
                Die();
                AwardExperience(instigator);
            }
            else
            {
                takeDamage.Invoke(damage);
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return baseStats.GetStat(Stat.Health);
        }


        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return healthPoints.value / baseStats.GetStat(Stat.Health);
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

        private void RegenerateHealth()
        {
            float regenHealthPoints = baseStats.GetStat(Stat.Health) * (regenerationPercentage / 100);
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);
        }

        public Type GetStateType()
        {
            return typeof(float);
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            InitComponents();

            healthPoints.value = (float)state;
            if (healthPoints.value == 0)
            {
                Die();
            }
        }


    }
}