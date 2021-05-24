using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PowerupDissolve : MonoBehaviour
{
    private GameObject player;
   // public GameObject ActivateEffect;
    public GameObject[] dissolveObject;
    private void Update()
    {
        ReleaseShip();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player");
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //Debug.Log("Freezed");
            ActivateButton();
            //Debug.Log("Returned from ActivateButton");
            if (CrossPlatformInputManager.GetButton("Jump") || Input.GetKey(KeyCode.Space))
            {
                //Debug.Log("Recorded input");
                other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }

        }
    }

    void ActivateButton ()
    {
        if (dissolveObject != null)
        {
            foreach (GameObject gameobject in dissolveObject)
                //gameObject.SetActive(false);
                Destroy(gameobject);
        }
    }
    void ReleaseShip()
    {
        if (CrossPlatformInputManager.GetButton("Jump") || Input.GetKey(KeyCode.Space))
        {
           // Debug.Log("Recorded input");
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}
