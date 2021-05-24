using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTrigger : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            other.GetComponent<Collider>().isTrigger = true;
            Destroy(other.gameObject, 5f);
        }

    }

}
