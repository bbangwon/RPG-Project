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
        ActionScheduler actionScheduler;

        Animator animator;
        Mover mover;
        Health health;

        float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
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

            if (!CanAttack(target)) return;

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
                TrggerAttack();
                timeSinceLastAttack = 0f;
            }
        }

        void TrggerAttack()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null) return false;
            if (combatTarget.IsDead()) return false;
            if (combatTarget.gameObject == gameObject) return false;
            if (health.IsDead()) return false;

            return true;
        }

        public void Attack(CombatTarget combatTarget)
        {
            this.target = combatTarget;
            actionScheduler.StartAction(this);
        }

        public void Cancel()
        {
            TriggerStopAttack();
            target = null;
        }

        void TriggerStopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        // Animation Event
        void Hit()
        {
            if (target == null) return;

            target.TakeDamage(weaponDamage);
        }
    }
}