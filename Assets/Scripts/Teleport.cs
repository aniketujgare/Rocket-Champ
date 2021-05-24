using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform destination;   // Gameobject were they were be teleported
    public string TagList = "|Player|"; // List of all tags that can teleport

    [Tooltip("How far player port from desti. on X-axis")]
    [SerializeField] float xoffSet = 2f;

    [Tooltip("How far player port from desti. on Y-axis")]
    [SerializeField] float yoffSet = 0f;

    [Tooltip("For Horizontal Put -90 & For Verticle Put 0")]
    [SerializeField] int DestRotateOnZ = -90;

    [Tooltip("Destination impart velocity in X direction (Default 1) ")]
    [SerializeField] float destVelocityX = 1f;

    [Tooltip("Destination impart velocity in Y direction (Default 0.3) ")]
    [SerializeField] float destVelocityY = 0.3f;

    // float xAxis = 0.5f;
    private float currentVelocity;

    public void OnTriggerEnter(Collider other)
    {
        Vector3 offset = new Vector3(xoffSet, yoffSet, 0f);
        Vector3 direction = new Vector3(destVelocityX, 0.3f, 0f)* 0.5f;

        //If the tag of the colliding object is allowed ti teleport
        if (TagList.Contains(string.Format("|{0}", other.tag)))
        {
            //Update other object position and rotation
            currentVelocity = other.attachedRigidbody.velocity.magnitude;
            other.transform.position = destination.transform.position + offset;
            other.attachedRigidbody.velocity = direction*currentVelocity;
            other.transform.rotation = destination.transform.rotation;
            other.transform.Rotate(0, 0, DestRotateOnZ);
            
        }
    }
}
