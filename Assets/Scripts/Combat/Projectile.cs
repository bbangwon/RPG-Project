using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float speed = 1f;

        CapsuleCollider targetCapsuleCollider;

        void Update()
        {
            if(target == null) return;

            transform.LookAt(GetAimLocation());
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }

        private Vector3 GetAimLocation()
        {
            //TODO: Ÿ���� �����Ҷ� ���� ����
            if(targetCapsuleCollider == null)
                targetCapsuleCollider = target.GetComponent<CapsuleCollider>();

            if (targetCapsuleCollider == null) 
                return target.position;

            return target.position + Vector3.up * targetCapsuleCollider.height / 2;
        }
    }

}