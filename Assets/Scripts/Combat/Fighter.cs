using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        CombatTarget target;

        Mover mover;

        ActionScheduler actionScheduler;

        Animator animator;

        float timeSinceLastAttack = 0f;

        private void Start()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }

        bool GetIsRange()
        {
            if (target == null) return false;
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            return distanceToTarget < weaponRange;
        }


        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            if (!GetIsRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                animator.ResetTrigger("stopAttack");
                animator.SetTrigger("attack");
                timeSinceLastAttack = 0f;               
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            this.target = combatTarget;
            actionScheduler.StartAction(this);
        }

        public void Cancel()
        {
            animator.SetTrigger("stopAttack");
            target = null;
        }

        // Animation Event
        void Hit()
        {
            if(target == null) return;

            target.TakeDamage(weaponDamage);
        }
    }
}