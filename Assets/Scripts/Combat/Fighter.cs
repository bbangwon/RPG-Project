using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;

        Transform target;
        Mover mover;

        ActionScheduler actionScheduler;

        Animator animator;

        private void Start()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }

        bool GetIsRange()
        {
            if (target == null) return false;
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            return distanceToTarget < weaponRange;
        }


        private void Update()
        {
            if (target == null) return;

            if (!GetIsRange())
            {
                mover.MoveTo(target.position);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        void AttackBehaviour()
        {
            animator.SetTrigger("attack");
        }

        public void Attack(CombatTarget combatTarget)
        {
            this.target = combatTarget.transform;
            actionScheduler.StartAction(this);
        }

        public void Cancel()
        {
            target = null;
        }

        // Animation Event
        void Hit()
        {

        }
    }
}