using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.3f;

        private void OnDrawGizmos()
        {            
            for (int i = 0; i < transform.childCount; i++)
            {
                var waypoint = transform.GetChild(i);
                Gizmos.DrawSphere(waypoint.position, waypointGizmoRadius);

                var nextWaypoint = i + 1 < transform.childCount ? transform.GetChild(i + 1) : transform.GetChild(0);
                Gizmos.DrawLine(waypoint.position, nextWaypoint.position);
            }
        }
    }

}