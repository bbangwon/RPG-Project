using RPG.Combat;
using RPG.Movement;
using System;
using UnityEditor.TerrainTools;
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
            //액션 우선 순위
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
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

                    //커서 어포던스(Affordance)를 제공하기 위해 if문 바깥으로 빼냄
                    //(어떤 행동을 유도하는 것) 커서를 바꿔줌
                    return true;
                }
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            if (Physics.Raycast(GetMouseRay(), out RaycastHit hit))
            {
                if (Input.GetMouseButton(0))
                {
                    mover.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}