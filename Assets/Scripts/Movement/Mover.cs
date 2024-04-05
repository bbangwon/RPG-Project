using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;
using RPG.Core;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        NavMeshAgent navMeshAgent;
        Animator animator;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        //이동 액션을 시작
        public void StartMoveAction(Vector3 destination)
        {
            //순환의존..
            GetComponent<Fighter>().Cancel();  
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.SetDestination(destination);
            navMeshAgent.isStopped = false;
        }

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            //로컬 방향 벡터로 변환
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            //z축(foward) 크기가 속도
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }
    }

}