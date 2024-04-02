using System;
using UnityEngine;
using UnityEngine.AI;

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
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
        UpdateAnimator();
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.point = new Vector3(hit.point.x, 0, hit.point.z);
            navMeshAgent.SetDestination(hit.point);
        }
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
