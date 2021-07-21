using UnityEngine;
using System.Collections;

public class PushEnable : MonoBehaviour
{
    public float pushForce = 1.0f;
    private void Update()
    {
        this.GetComponent<Rigidbody>().AddForce(0* pushForce, 0, 0 , ForceMode.Force);
    }
}