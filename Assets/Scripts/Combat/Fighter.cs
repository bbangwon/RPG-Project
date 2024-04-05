using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 2f;

        Transform target;
        Mover mover;

        private void Start()
        {
            mover = GetComponent<Mover>();
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
                mover.Stop();
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            this.target = combatTarget.transform;
            GetComponent<ActionScheduler>().StartAction(this);
        }

        public void Cancel()
        {
            target = null;
        }
    }
}