using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        CombatTarget target;
        float damage = 0;

        CapsuleCollider targetCapsuleCollider;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        void Update()
        {
            if(target == null) return;

            if(isHoming && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }

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
            if (target.IsDead()) return;
            target.TakeDamage(damage);

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }
            Destroy(gameObject);
        }
    }

}