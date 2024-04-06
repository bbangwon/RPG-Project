using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        GameObject player;

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (DistanceToPlayer() < chaseDistance)
            {
                Debug.Log($"{name} : Player is close, chase now!");
            }          
        }

        float DistanceToPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }
}