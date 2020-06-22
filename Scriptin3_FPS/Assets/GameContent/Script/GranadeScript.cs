using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeScript : MonoBehaviour
{
    [SerializeField] float damage = 80;
    [SerializeField] float duration = 2;
    [SerializeField] float radius = 4;
    [SerializeField] float force = 20;
    [SerializeField] GameObject explosion;

    // Update is called once per frame
    void Update()
    {
        if (duration <= 0)
        {
            Explode();
        }

        duration -= Time.deltaTime;
    }

    private void Explode()
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
