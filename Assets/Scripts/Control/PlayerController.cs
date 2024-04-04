using RPG.Combat;
using RPG.Movement;
using System;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Fighter fighter;

        private void Awake()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        // Update is called once per frame
        void Update()
        {
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                //hit.transform을 사용하면 rigidbody또는 collider를 모두 사용하여 감지함
                if(hit.transform.TryGetComponent(out CombatTarget target))
                {
                    if (target == null) continue;

                    if(Input.GetMouseButtonDown(0))
                    {
                        fighter.Attack(target);
                    }
                }
            }
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            if (Physics.Raycast(GetMouseRay(), out RaycastHit hit))
            {
                mover.MoveTo(hit.point);
            }
        }

        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}