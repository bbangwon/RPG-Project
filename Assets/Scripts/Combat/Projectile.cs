using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        CombatTarget target;
        float damage = 0;

        CapsuleCollider targetCapsuleCollider;

        void Update()
        {
            if(target == null) return;

            transform.LookAt(GetAimLocation());
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }

        public void SetTarget(CombatTarget target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        private Vector3 GetAimLocation()
        {
            //TODO: 타겟을 세팅할때 같이 세팅
            if(targetCapsuleCollider == null)
                targetCapsuleCollider = target.GetComponent<CapsuleCollider>();

            if (targetCapsuleCollider == null) 
                return target.transform.position;

            return target.transform.position + Vector3.up * targetCapsuleCollider.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CombatTarget>() != target) return;
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}