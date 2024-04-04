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
            //�׼� �켱 ����
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                //hit.transform�� ����ϸ� rigidbody�Ǵ� collider�� ��� ����Ͽ� ������
                if(hit.transform.TryGetComponent(out CombatTarget target))
                {
                    if (target == null) continue;

                    if(Input.GetMouseButtonDown(0))
                    {
                        fighter.Attack(target);                        
                    }

                    //Ŀ�� ��������(Affordance)�� �����ϱ� ���� if�� �ٱ����� ����
                    //(� �ൿ�� �����ϴ� ��) Ŀ���� �ٲ���
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