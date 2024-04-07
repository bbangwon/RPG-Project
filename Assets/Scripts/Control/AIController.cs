using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        Fighter fighter;
        Health health;
        Mover mover;

        CombatTarget player;

        Vector3 guardLocation;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
        }

        private void Start()
        {
            player = GameObject.FindWithTag("Player")
                .GetComponent<CombatTarget>();

            guardLocation = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) return;
            InteractWithCombat();
        }

        private void InteractWithCombat()
        {
            if (!fighter.CanAttack(player)) return;
            if (InAttackRangeOfPlayer())
            {
                fighter.Attack(player);
            }
            else
            {
                mover.StartMoveAction(guardLocation);
            }
        }

        bool InAttackRangeOfPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}