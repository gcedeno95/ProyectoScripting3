using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    GameObject player;
    /*Vector3 origin;
    float distanceToPlayer;
    [SerializeField] float distanceToDetect = 10;
    [SerializeField] float distanceToAttack = 2;
    [SerializeField] float timeBtwAttacks = 1;
    [SerializeField] Animator enemyAnimator;
    float timerAttacks;
    float speed;*/

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //origin = transform.position;
        //timerAttacks = 0;
    }

    // Update is called once per frame
    /*void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);

        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(targetPosition);

        if (distanceToPlayer <= distanceToAttack && timerAttacks >= timeBtwAttacks)
        {
            enemyAnimator.SetFloat("Speed", 0);
            enemyAnimator.SetTrigger("Attack");
            player.GetComponent<Life>().TakeDamage(10);
            timerAttacks = 0;
        }
        else
        {
            enemyAnimator.SetFloat("Speed", 1);
            timerAttacks += Time.deltaTime;
        }
        
    }*/

    public void DoDamage()
    {
        player.GetComponent<Life>().TakeDamage(10);
    }
}
