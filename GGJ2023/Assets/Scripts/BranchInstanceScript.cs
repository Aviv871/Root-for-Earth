using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchInstanceScript : MonoBehaviour
{

    public Transform headTransform;

    public bool isGrowing = true;
    public int BranchTimeToLive = 500;

    void Awake()
    {
        headTransform = GetComponentInChildren<BranchHeadScript>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        BranchTimeToLive--;
        if (BranchTimeToLive == 0) {
           isGrowing = false;
           GetComponentInChildren<BranchTailScript>().isDrawing = false;
        }   
    }
}
