using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryScript : MonoBehaviour
{
    public float factorySpawnRate = 5;
    public GameObject factory;

    public GameObject planet;

    private float radius;

    private float spawnCounter;
    private float factoryRadius;

    // Start is called before the first frame update0
    void Start()
    {
        factoryRadius = Mathf.Sqrt(Mathf.Pow(factory.GetComponent<BoxCollider2D>().size.x/2, 2) + Mathf.Pow(factory.GetComponent<BoxCollider2D>().size.y/2, 2));
        Debug.Log(factoryRadius);
        radius = planet.GetComponent<CircleCollider2D>().radius;
        spawnCounter = factorySpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCounter >= factorySpawnRate) 
        {
            int degrees = Random.Range(0, 360);
            Vector3 position = generatePosition(degrees);
            if (!Physics2D.OverlapCircle(position, factoryRadius * 2, LayerMask.GetMask("FactoryLayer")))
            {
                Debug.Log("overlap");
                Instantiate(factory, position, Quaternion.AngleAxis(degrees - 90, transform.forward)); 
                spawnCounter = 0;
            }

        } 
        else 
        {
            spawnCounter+= Time.deltaTime;
        }
    }

    private Vector3 generatePosition(int degrees) 
    {
        float radians = degrees * Mathf.Deg2Rad;
        float x = Mathf.Cos(radians) * radius;
        float y = Mathf.Sin(radians) * radius;
        return new Vector3(x, y, 0);
    }
}
