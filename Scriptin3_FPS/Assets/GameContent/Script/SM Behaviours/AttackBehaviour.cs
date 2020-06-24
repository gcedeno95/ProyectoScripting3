using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    GameObject player;
    float distanceToPlayer;
    [SerializeField] float distanceToDetect = 10;
    [SerializeField] float distanceToAttack = 2;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distanceToPlayer = Vector3.Distance(animator.transform.position, player.transform.position);

        if (distanceToPlayer > distanceToAttack)
        {
            animator.SetBool("Attack", false);
        }
        if (distanceToPlayer > distanceToDetect)
        {
            animator.SetBool("Chasing", false);
        }
    }
}
