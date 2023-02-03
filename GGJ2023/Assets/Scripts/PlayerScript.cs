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
        StartCoroutine(DelayedDestroy(obj));

        Collectable collectable = obj.GetComponent<Collectable>();
    
        switch (collectable.item) {
            case CollectableItem.WATER:
                StartCoroutine(BoostSpeed());
                break;
        }
    }

    // We delay the destroyment of the object because of its sound effects
    public IEnumerator DelayedDestroy(GameObject obj) {
        obj.GetComponent<SpriteRenderer>().enabled = false;
        obj.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(5);
        Destroy(obj);
    }

    public IEnumerator BoostSpeed() {
        GetComponentInChildren<HeadScript>().forwardSpeed *= speedBoostFactor;
        yield return new WaitForSeconds(speedBoostTime);
        GetComponentInChildren<HeadScript>().forwardSpeed /= speedBoostFactor;

    }

    public void Respawn(GameObject originTree) {
        Debug.Log("RESPAWNING");
        TailScript tailScript = gameObject.GetComponentInChildren<TailScript>();
        if (tailScript) {
            // So we don't collide with the old tail, we do it outside the coroutine
		    tailScript.gameObject.GetComponent<EdgeCollider2D>().enabled = false;
            
            // Fade out the old tail
            StartCoroutine(tailScript.FadeOutAndDestroy());
        }
        
        GameObject newTail = Instantiate(tailObject, Vector3.zero, Quaternion.identity, transform);
        
        // Disable the collider temporarily
        newTail.GetComponent<EdgeCollider2D>().enabled = false;
        StartCoroutine(EnableColliderLater(newTail, 2f));

        newTail.GetComponent<TailScript>().originTree = originTree;
        headTransform.Rotate(new Vector3(0, 0, 180));
    }

    public void DestroyedFactory()
    {
        score += logicManager.factoryScore;
    }

    public IEnumerator EnableColliderLater(GameObject tail, float seconds) {
        yield return new WaitForSeconds(seconds);
        tail.GetComponent<EdgeCollider2D>().enabled = true;
    }
    
}
