using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public bool isAlive = true;

    public Transform headTransform;
    private LogicManagerScript logicManager;

    void Awake()
    {
        headTransform = GetComponentInChildren<HeadScript>().transform;
        logicManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collision() {
        isAlive = false;
        logicManager.GameOver();
    }
}
