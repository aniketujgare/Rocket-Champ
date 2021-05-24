using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundries : MonoBehaviour
{
    private Transform topBoundry;
    private Transform leftBoundry;
    private Transform rightBoundry;
    public GameObject[] Friendly;

    
    

    // Start is called before the first frame update
    void Start()
    {// Temp
        Friendly = GameObject.FindGameObjectsWithTag("Friendly");
        //
        topBoundry = GameObject.Find("Boundry Top").transform;
        leftBoundry = GameObject.Find("Boundry Left").transform;
        rightBoundry = GameObject.Find("Boundry Right").transform;

        float screenratio = Screen.width * 1f / Screen.height;

        if (screenratio < 1.8f)                                             // 16:9     1.77
        {
            topBoundry.transform.localPosition = new Vector3(23.89004f, 28.2f, 1.12446f);
            leftBoundry.transform.localPosition = new Vector3(-11.7f, 9.703335f, 1.12446f);
            rightBoundry.transform.localPosition = new Vector3(56.1f, 9.903334f, 1.12446f);
        }

        else if (screenratio > 1.81f && screenratio <= 2.01f)                  // 18:9     2
        {
            topBoundry.transform.localPosition = new Vector3(23.89004f, 27.31f, 1.12446f);
            leftBoundry.transform.localPosition = new Vector3(-15.9f, 9.703335f, 1.12446f);
            rightBoundry.transform.localPosition = new Vector3(60.09f, 9.903334f, 1.12446f);
        }
        else if (screenratio > 2.10f && screenratio < 2.20f)                 // 19.5:9   2.16
        {
            topBoundry.transform.localPosition = new Vector3(23.89004f, 27.21f, 1.12446f);
            leftBoundry.transform.localPosition = new Vector3(-18.9f, 9.703335f, 1.12446f);
            rightBoundry.transform.localPosition = new Vector3(63.2f, 9.903334f, 1.12446f);
        }
        else                                                                 // 20:9      2.22
        {
            topBoundry.transform.localPosition = new Vector3(23.89004f, 27.2f, 1.12446f);
            leftBoundry.transform.localPosition = new Vector3(-19.9f, 9.703335f, 1.12446f);
            rightBoundry.transform.localPosition = new Vector3(63.2f, 9.903334f, 1.12446f);

        }

    }
}
