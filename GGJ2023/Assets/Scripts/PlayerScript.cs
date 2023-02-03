using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float speedBoostFactor = 1.5f;
    public float speedBoostTime = 3f;
    public bool isAlive = true;

    public Transform headTransform;
    public float score = 0;
    public float scoreSpeed = 1;
    private LogicManagerScript logicManager;

    public GameObject tailObject;

    void Awake()
    {
        headTransform = GetComponentInChildren<HeadScript>().transform;
        logicManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // individual score
        if (isAlive)
        {
            score += Time.deltaTime * scoreSpeed * GetComponentInChildren<HeadScript>().forwardSpeed;
        }
    }

    public void Collision() {
        isAlive = false;
    }

    public void Collect(GameObject obj) {
        StartCoroutine(delayedDestroy(obj));

        Collectable collectable = obj.GetComponent<Collectable>();
    
        switch (collectable.item) {
            case CollectableItem.WATER:
                StartCoroutine(boostSpeed());
                break;
        }
    }

    // We delay the destroyment of the object because of its sound effects
    public IEnumerator delayedDestroy(GameObject obj) {
        obj.GetComponent<SpriteRenderer>().enabled = false;
        obj.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(5);
        Destroy(obj);
    }

    public IEnumerator boostSpeed() {
        GetComponentInChildren<HeadScript>().forwardSpeed *= speedBoostFactor;
        yield return new WaitForSeconds(speedBoostTime);
        GetComponentInChildren<HeadScript>().forwardSpeed /= speedBoostFactor;

    }

    public void Respawn(GameObject originTree) {
        TailScript tailScript = gameObject.GetComponentInChildren<TailScript>();
        if (tailScript) {
            StartCoroutine(tailScript.FadeOutAndDestroy());
        }
        
        GameObject newTail = Instantiate(tailObject, Vector3.zero, Quaternion.identity, transform);
        newTail.GetComponent<TailScript>().originTree = originTree;
        headTransform.Rotate(new Vector3(0, 0, 180));
    }
}
