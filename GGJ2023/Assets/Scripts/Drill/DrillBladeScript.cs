using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillBladeScript : MonoBehaviour
{
    private GameObject target;
    private float speed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0) {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == target.transform)
        {
            GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>().GameOver();
        }
        else if (other.tag == "Obstacle")
        {
            Stopped();
        }
    }

    private void Stopped()
    {
        speed = 0;
        StartCoroutine(DelayDestroy());
    }

    IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
