using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] PatrolPath patrolPath;

        [SerializeField] float waypointTolerance = 1f;

        Fighter fighter;
        Health health;
        Mover mover;

        CombatTarget player;

        Vector3 guardLocation;

        ActionScheduler actionScheduler;

        int currentWaypointIndex = 0;

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
                PatrolBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardLocation;

            if(patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            mover.StartMoveAction(nextPosition);
        }

        private bool AtWaypoint()
        {
            return Vector3.Distance(transform.position, GetCurrentWaypoint()) < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);            
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
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