using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryScript : MonoBehaviour
{
    public float factorySpawnRate = 10;
    public GameObject factory;

    public GameObject planet;

    private float radius;

    private float spawnCounter;

    // Start is called before the first frame update
    void Start()
    {
        radius = Vector3.Distance(this.transform.position, this.transform.GetChild(0).transform.position);
        spawnCounter = factorySpawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCounter == factorySpawnRate) 
        {
            int degrees = Random.Range(0, 360);
            float radians = degrees * Mathf.Deg2Rad;
            float x = Mathf.Cos(radians);
            float y = Mathf.Sin(radians);
            Vector3 position = new Vector3(x, y, 0);
            Instantiate(factory, position, Quaternion.AngleAxis(degrees, transform.forward));
            spawnCounter = 0;
        } 
        else 
        {
            spawnCounter+= Time.deltaTime;
        }
    }
}
