using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float xPos, yPos, zPos;
    public float xVelo, yVelo, zVelo;
    public GameObject newgameObject;

    void Update ()
    {
        ObjectMove();
    }
    void ObjectMove ()
    {

        //newgameObject.transform.Translate(Time.deltaTime, 0, 0);
        for (float i = 0; i < xPos; i++)
        {
            //Debug.Log(i);
            newgameObject.transform.position += new Vector3(xVelo * Time.deltaTime, 0, 0);
        }
    }
}
