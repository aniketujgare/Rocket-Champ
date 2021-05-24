using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRatio : MonoBehaviour
{
    public RectTransform shipPanel;
    // Start is called before the first frame update
    void Start()
    {
        float screenratio = Screen.width * 1f / Screen.height;

        if (screenratio < 1.8f)                                             // 16:9     1.77
        {
            this.transform.position = new Vector3(7.55f, 3.38f, -8.69f);
            shipPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0, -1299.271f);
        }
        else if (screenratio > 1.81f && screenratio <= 2.01f)                  // 18:9     2
        {
            this.transform.position = new Vector3(8.57f, 3.38f, -8.69f);
            shipPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0, -903.6114f);
        }
        else if (screenratio > 2.10f && screenratio < 2.20f)                 // 19.5:9   2.16
        {
            this.transform.position = new Vector3(9.14f, 3.38f, -8.69f);
            shipPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0, -933.7305f);
        }
        else
        {
            this.transform.position = new Vector3(9.46f, 3.38f, -8.69f);         // 20:9      2.22
            shipPanel.GetComponent<RectTransform>().offsetMin = new Vector2(0, -933.7305f);
        }
    }
}
