using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseBehavior : StateMachineBehaviour
{
    GameObject player;
    Vector3 origin;
    float distanceToPlayer;
    [SerializeField] float distanceToDetect = 10;
    [SerializeField] float distanceToAttack = 2;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        origin = animator.transform.position;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        animator.GetComponentInParent<NavMeshAgent>().SetDestination(player.transform.position);
        distanceToPlayer = Vector3.Distance(animator.transform.position, player.transform.position);

        Vector3 targetPosition = new Vector3(player.transform.position.x, animator.transform.position.y, player.transform.position.z);
        animator.transform.LookAt(targetPosition);

        if (distanceToPlayer <= distanceToAttack)
        {
            animator.SetBool("Attack", true);
        }
        else if (distanceToPlayer > distanceToDetect)
        {
            animator.SetBool("Chasing", false);
        }
    }
}
