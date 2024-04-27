using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        CombatTarget target;

        CapsuleCollider targetCapsuleCollider;

        void Update()
        {
            if(target == null) return;

            transform.LookAt(GetAimLocation());
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }

        public void SetTarget(CombatTarget target)
        {
            this.target = target;
        }

        private Vector3 GetAimLocation()
        {
            //TODO: Ÿ���� �����Ҷ� ���� ����
            if(targetCapsuleCollider == null)
                targetCapsuleCollider = target.GetComponent<CapsuleCollider>();

            if (targetCapsuleCollider == null) 
                return target.transform.position;

            return target.transform.position + Vector3.up * targetCapsuleCollider.height / 2;
        }
    }

}