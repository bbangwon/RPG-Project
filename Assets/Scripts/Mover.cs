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
        //로컬 방향 벡터로 변환
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        //z축(foward) 크기가 속도
        float speed = localVelocity.z;
        animator.SetFloat("forwardSpeed", speed);
    }
}
