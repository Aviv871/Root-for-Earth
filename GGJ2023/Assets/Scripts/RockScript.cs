using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    public int obstacleCount = 5;

    public GameObject obstacle;

    // Start is called before the first frame update
    void Start()
    {
        float distance = Vector3.Distance(this.transform.position, this.transform.GetChild(0).transform.position);
        float obstacleRadius = obstacle.GetComponent<Collider2D>().bounds.extents.x;

        for (int i = 0; i < obstacleCount; i++)
        {
            Vector3 location = Random.insideUnitCircle * distance;
            Collider2D CollisionWithEnemy = Physics2D.OverlapCircle(location, obstacleRadius, LayerMask.GetMask("ObstacleLayer"));
            //If the Collision is empty then, we can instantiate
            if (CollisionWithEnemy == false)
                Instantiate(obstacle, location, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
