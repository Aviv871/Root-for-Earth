using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Transform headTransform;

    // Start is called before the first frame update
    void Start()
    {
        headTransform = GetComponentInChildren<HeadScript>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
