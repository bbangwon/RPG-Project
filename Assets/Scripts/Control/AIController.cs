using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;

        Fighter fighter;
        Health health;
        Mover mover;

        CombatTarget player;

        Vector3 guardLocation;

        ActionScheduler actionScheduler;

        [SerializeField]
        float timeSinceLastSawPlayer = Mathf.Infinity;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            actionScheduler = GetComponent<ActionScheduler>();
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
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if(timeSinceLastSawPlayer < suspicionTime)
            {
                //의심 구간...
                SuspicionBehaviour();
            }
            else
            {
                GuardBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            mover.StartMoveAction(guardLocation);
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private void SuspicionBehaviour()
        {
            actionScheduler.CancelCurrentAction();
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