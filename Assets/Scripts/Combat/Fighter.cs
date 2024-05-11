using RPG.Attributes;
using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using RPG.Stats;
using System;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform;
        [SerializeField] Transform leftHandTransform;
        [SerializeField] Weapon defaultWeapon;

        CombatTarget target;
        ActionScheduler actionScheduler;

        BaseStats baseStats;
        Animator animator;
        Mover mover;
        Health health;

        float timeSinceLastAttack = Mathf.Infinity;

        Weapon currentWeapon = null;

        private void Awake()
        {
            InitComponents();
        }

        private void Start()
        {         
            if(currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (!CanAttack(target)) return;

            if (!GetIsRange())
            {
                mover.MoveTo(target.transform.position, 1f);
            }
            else
            {
                mover.Cancel();
                AttackBehaviour();
            }
        }

        private void InitComponents()
        {
            if (mover == null)
                mover = GetComponent<Mover>();
            if (actionScheduler == null)
                actionScheduler = GetComponent<ActionScheduler>();
            if (animator == null)
                animator = GetComponent<Animator>();
            if (health == null)
                health = GetComponent<Health>();
            if (baseStats == null)
                baseStats = GetComponent<BaseStats>();
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            if (weapon == null) return;
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public CombatTarget GetTarget()
        {
            return target;
        }

        bool GetIsRange()
        {
            if (target == null) return false;
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            return distanceToTarget < currentWeapon.GetRange();
        }

        void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                TrggerAttack();
                timeSinceLastAttack = 0f;
            }
        }

        void TrggerAttack()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null) return false;
            if (combatTarget.IsDead()) return false;
            if (combatTarget.gameObject == gameObject) return false;
            if (health.IsDead()) return false;

            return true;
        }

        public void Attack(CombatTarget combatTarget)
        {
            this.target = combatTarget;
            actionScheduler.StartAction(this);
        }

        public void Cancel()
        {
            TriggerStopAttack();
            mover.Cancel();
            target = null;
        }

        void TriggerStopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        // Animation Event
        void Hit()
        {
            if (target == null) return;

            float damage = baseStats.GetStat(Stat.Damage);            

            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject, damage);
            }
            else
            {
                target.TakeDamage(gameObject, damage);
            }
        }

        void Shoot()
        {
            Hit();
        }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            InitComponents();

            string weaponName = (string)state;
            Weapon weapon = Resources.Load<Weapon>(weaponName);
            EquipWeapon(weapon);
        }

        public Type GetStateType()
        {
            return typeof(string);
        }
    }
}