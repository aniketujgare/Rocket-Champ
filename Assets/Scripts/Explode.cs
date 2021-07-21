using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public GameObject explosion;
    public GameObject smoke;
    public int maximumSmoke;
    public float destroyDelay;
    public float minForce;
    public float maxForce;
    public float radius;

    void Start()
    {
        Explose ();
    }
    public void Explose ()
    {
        int smokeCounter = 0;

        if (explosion != null)
        {
            GameObject explosionFX = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(explosionFX, 5);
        }

        foreach (Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            }

            if (smoke != null && smokeCounter < maximumSmoke)
            {
                if ( Random.Range(1,4)==1)
                {
                    //print("range");
                    GameObject smokeFX = Instantiate(smoke, t.transform.position , Quaternion.identity) as GameObject;
                    smokeCounter ++;
                   // Destroy(smokeFX, 5);
                }
            }
            //Destroy(t.gameObject, destroyDelay);
        }
    }
}
