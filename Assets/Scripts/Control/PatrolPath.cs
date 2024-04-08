using System;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.3f;

        public Vector3 GetWaypoint(int currentWaypointIndex)
        {
            return transform.GetChild(currentWaypointIndex).position;            
        }

        public int GetNextIndex(int currentWaypointIndex)
        {
            return (currentWaypointIndex + 1) % transform.childCount;
        }

        private void OnDrawGizmos()
        {            
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);

                int nextIndex = GetNextIndex(i);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(nextIndex));
            }
        }
    }

}