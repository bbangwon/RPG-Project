using RPG.Core;
using UnityEditor.U2D;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] destoryOnHit = null;
        [SerializeField] float lifeAfterImpact = 2f;

        CombatTarget target;
        GameObject instigator = null;
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

        public void SetTarget(CombatTarget target, GameObject instigator, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;

            Destroy(gameObject, maxLifeTime);
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
            target.TakeDamage(instigator, damage);

            speed = 0;

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            if (destoryOnHit != null)
            {
                foreach (var toDestroy in destoryOnHit)
                {
                    Destroy(toDestroy);
                }
            }

            Destroy(gameObject, lifeAfterImpact);
        }
    }

}