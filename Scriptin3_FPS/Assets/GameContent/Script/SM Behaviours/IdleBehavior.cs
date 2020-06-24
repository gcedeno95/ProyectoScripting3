using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleBehavior : StateMachineBehaviour
{
    GameObject player;
    Vector3 origin;
    float distanceToPlayer;
    [SerializeField] float distanceToDetect = 10;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        origin = animator.transform.position;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distanceToPlayer = Vector3.Distance(animator.transform.position, player.transform.position);

        if(distanceToPlayer <= distanceToDetect)
        {
            animator.SetBool("Chasing", true);
        }
     
    }
}
