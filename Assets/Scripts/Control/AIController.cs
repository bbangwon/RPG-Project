using RPG.Combat;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        CombatTarget player;
        Fighter fighter;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
        }

        private void Start()
        {
            player = GameObject.FindWithTag("Player")
                .GetComponent<CombatTarget>();
        }

        private void Update()
        {
            if (!fighter.CanAttack(player)) return;
            if (InAttackRangeOfPlayer())
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        bool InAttackRangeOfPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
        }
    }
}