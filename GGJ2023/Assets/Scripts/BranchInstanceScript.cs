using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchInstanceScript : MonoBehaviour
{

    public Transform headTransform;

    public bool isGrowing = true;
    public float BranchTimeToLive;

    void Awake()
    {
        BranchTimeToLive = Random.Range(5, 40) / 10;
        headTransform = GetComponentInChildren<BranchHeadScript>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        BranchTimeToLive -= Time.deltaTime;
        if (BranchTimeToLive <= 0) {
           isGrowing = false;
           GetComponentInChildren<BranchTailScript>().isDrawing = false;
        }   
    }
}
