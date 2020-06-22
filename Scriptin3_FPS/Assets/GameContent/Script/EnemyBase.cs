using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    GameObject player;
    Vector3 origin;
    float distanceToPlayer;
    [SerializeField] float distanceToDetect = 10;
    [SerializeField] float distanceToAttack = 2;
    [SerializeField] float distanceToOrigin = 15;
    [SerializeField] float timeBtwAttacks = 1;
    float timerAttacks;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        origin = transform.position;
        timerAttacks = 0;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
        if (distanceToPlayer <= distanceToAttack && timerAttacks >= timeBtwAttacks)
        {
            player.GetComponent<Life>().TakeDamage(10);
            timerAttacks = 0;
        }
        else
        {
            timerAttacks += Time.deltaTime;
        }
        
    }
}
