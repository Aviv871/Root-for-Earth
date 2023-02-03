using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float speedBoostFactor = 1.5f;
    public float speedBoostTime = 3f;
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

    public void Collect(GameObject obj) {
        Collectable collectable = obj.GetComponent<Collectable>();
        Destroy(obj);
        Debug.Log(nameof(collectable.item));
        switch (collectable.item) {
            case CollectableItem.WATER:
                StartCoroutine("boostSpeed");
                break;
        }
    }

    public IEnumerator boostSpeed() {
        GetComponentInChildren<HeadScript>().forwardSpeed *= speedBoostFactor;
        yield return new WaitForSeconds(speedBoostTime);
        GetComponentInChildren<HeadScript>().forwardSpeed /= speedBoostFactor;

    }
}
