using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject enemyToSpawn;
    [SerializeField] float timeBtwSpawn;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = timeBtwSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= timeBtwSpawn)
        {
            Instantiate(enemyToSpawn, this.transform);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
