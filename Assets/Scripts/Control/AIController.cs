using RPG.Attributes;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] PatrolPath patrolPath;

        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 3f;

        [Range(0, 1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;

        Fighter fighter;
        Health health;
        Mover mover;

        CombatTarget player;

        Vector3 guardLocation;

        ActionScheduler actionScheduler;

        int currentWaypointIndex = 0;
        float timeSinceLastSawPlayer = Mathf.Infinity;

        float timeSinceArrivedAtWaypoint = Mathf.Infinity;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();

            actionScheduler = GetComponent<ActionScheduler>();

            player = GameObject.FindWithTag("Player")
                .GetComponent<CombatTarget>();
        }

        private void Start()
        {
            guardLocation = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) return;
            InteractWithCombat();
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void InteractWithCombat()
        {
            if (!fighter.CanAttack(player)) return;
            if (InAttackRangeOfPlayer())
            {                
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

            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }
        }

        private bool AtWaypoint()
        {
            return Vector3.Distance(transform.position, GetCurrentWaypoint()) < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
            timeSinceArrivedAtWaypoint = 0;
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
            timeSinceLastSawPlayer = 0;
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