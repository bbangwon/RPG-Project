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

        private void Start()
        {
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
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
            }
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
    }
}