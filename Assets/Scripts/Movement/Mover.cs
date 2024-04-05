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

        //�̵� �׼��� ����
        public void StartMoveAction(Vector3 destination)
        {
            //��ȯ����..
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
            //���� ���� ���ͷ� ��ȯ
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            //z��(foward) ũ�Ⱑ �ӵ�
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }
    }

}