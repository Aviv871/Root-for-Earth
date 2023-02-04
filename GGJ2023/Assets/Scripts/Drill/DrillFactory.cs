using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillFab : MonoBehaviour
{
    public float drillSpawnRate = 5;
    public GameObject drill;
    public GameObject planet;

    private float radius;

    private float spawnCounter;
    private float drillRadius;


    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D drillBoxCollider = drill.GetComponentInChildren<BoxCollider2D>();
        drillRadius = Mathf.Sqrt(Mathf.Pow(drillBoxCollider.size.x / 2, 2) + Mathf.Pow(drillBoxCollider.size.y / 2, 2));
        radius = planet.GetComponent<CircleCollider2D>().radius;
        spawnCounter = drillSpawnRate;
    }

    private Vector3 generatePosition(int degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float x = Mathf.Cos(radians) * radius;
        float y = Mathf.Sin(radians) * radius;
        return new Vector3(x, y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCounter >= drillSpawnRate)
        {
            int degrees = Random.Range(0, 360);
            Vector3 position = generatePosition(degrees);
            if (!Physics2D.OverlapCircle(position, drillRadius * 2, LayerMask.GetMask("DrillLayer")))
            {
                Instantiate(drill, position, Quaternion.AngleAxis(degrees - 90, transform.forward));
                spawnCounter = 0;
            }

        }
        else
        {
            spawnCounter += Time.deltaTime;
        }
    }
}
