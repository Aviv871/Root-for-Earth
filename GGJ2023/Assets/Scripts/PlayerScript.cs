using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float speedBoostFactor = 3f;
    public float speedBoostTime = 3f;
    public bool isAlive = false;

    public Transform headTransform;
    public float score = 0;
    public float scoreSpeed = 1;
    private LogicManagerScript logicManager;

    public GameObject tailObject;

    public Color color;

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
            score += Time.deltaTime * scoreSpeed * GetComponentInChildren<HeadScript>().totalForwardSpeed;
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
        obj.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(5);
        Destroy(obj);
    }

    public IEnumerator BoostSpeed() {
        GetComponentInChildren<HeadScript>().waterSpeedBoost = speedBoostFactor;
        yield return new WaitForSeconds(speedBoostTime);
        GetComponentInChildren<HeadScript>().waterSpeedBoost = 1;

    }

    public void Respawn(GameObject originTree) {
        TailScript tailScript = gameObject.GetComponentInChildren<TailScript>();
        if (tailScript) {
            // So we don't collide with the old tail, we do it outside the coroutine
            Collider2D col = tailScript.gameObject.GetComponent<EdgeCollider2D>();
		    col.enabled = false;            

            // Old tail shouldn't follow the new head
            tailScript.DisableDrawing();

            // Fade out the old tail
            tailScript.BeginFadingOut();
        }
        
        headTransform.Rotate(new Vector3(0, 0, 180));
        
        GameObject newTail = Instantiate(tailObject, Vector3.zero, Quaternion.identity, transform);
        newTail.GetComponent<TailScript>().originTree = originTree;
        newTail.GetComponent<Renderer>().material.color = color;
    }

    public void DestroyedFactory()
    {
        score += logicManager.factoryScore;
    }

}
