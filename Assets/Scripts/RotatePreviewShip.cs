using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePreviewShip : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = -100f;

    void Update()
    {
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
