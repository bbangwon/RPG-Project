using RPG.Attributes;
using UnityEngine;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {
        Health health;
        private void Awake()
        {
            health = GetComponent<Health>();
        }

        public Health GetHealth()
        {
            return health;
        }

        public bool IsDead()
        {
            return health.IsDead();
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            health.TakeDamage(instigator, damage);
        }


    }
}