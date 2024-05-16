using RPG.Attributes;
using RPG.Control;
using UnityEngine;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
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

        public bool HandleRaycast(PlayerController callingController)
        {
            Fighter fighter = callingController.GetComponent<Fighter>();

            if (!fighter.CanAttack(this)) return false;
            if (Input.GetMouseButton(0))
            {
                fighter.Attack(this);
            }            

            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }
    }
}