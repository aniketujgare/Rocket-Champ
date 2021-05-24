using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class ExplosionBar : MonoBehaviour
{
    private Transform Outerbox;
    public float destroyDelay;
    public float minForce;
    public float maxForce;
    public float radius;

    void Start()
    {
        Explose();
    }
    public void Explose()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);

        foreach (Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();
            t.gameObject.tag = "Friendly";
            t.GetComponent<Rigidbody>().useGravity =true;
          //  t.GetComponent<MeshCollider>().convex =true;


            if (rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            }
            Destroy(gameObject, 5f);

        }
    }
}
