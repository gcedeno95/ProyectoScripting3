using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    public bool canShoot;
    float damage = 110;
    [SerializeField] float speed = 5;
    [SerializeField] float radius = 5;
    [SerializeField] float force = 3;
    [SerializeField] GameObject explosion;

    private void Awake()
    {
        canShoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            GetComponent<Rigidbody>().velocity = -transform.forward * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canShoot)
        {
            Collider[] affectedGO = Physics.OverlapSphere(transform.position, radius);
            for (int i = 0; i < affectedGO.Length; i++)
            {
                if (affectedGO[i].gameObject.GetComponent<Life>())
                    affectedGO[i].gameObject.GetComponent<Life>().TakeDamage(damage);

                if (affectedGO[i].gameObject.GetComponent<Rigidbody>())
                    affectedGO[i].gameObject.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius);
            }
            GameObject theExplosion = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(theExplosion, 4);
            Destroy(this.gameObject);
        }
    }
}
