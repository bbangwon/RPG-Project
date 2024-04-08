using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        Animator animator;
        ActionScheduler actionScheduler;

        bool isDead = false;
        public bool IsDead()
        {
            return isDead;
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            Debug.Log(healthPoints);

            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            animator.SetTrigger("die");
            actionScheduler.CancelCurrentAction();
        }
    }
}