using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Transform headTransform;

    void Awake()
    {
        headTransform = GetComponentInChildren<HeadScript>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
