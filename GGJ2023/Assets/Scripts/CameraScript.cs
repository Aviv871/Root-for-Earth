using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Camera cm;

    private LogicManagerScript logicManager;

    // Start is called before the first frame update
    void Start()
    {
        cm = GetComponent<Camera>();
        logicManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.Rotate(new Vector3(0,0,0.1f));

        if (logicManager.factoryCount > 3)
        {
            ChangeBrightness(cm.backgroundColor, 1f - 0.05f * (float)logicManager.factoryCount);
        }

    }

    void ChangeBrightness(Color startColor, float value)
    {
        float h, s, v;
        Color.RGBToHSV(startColor, out h, out s, out v);
        Color endColor = Color.HSVToRGB(h, s, value);
        cm.backgroundColor = Color.Lerp(startColor, endColor, Time.deltaTime * 10);
    }
}
