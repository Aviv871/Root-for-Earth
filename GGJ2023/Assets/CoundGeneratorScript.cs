using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoundGeneratorScript : MonoBehaviour
{
    public GameObject cloud;
    public Vector3 basePosition;
    public float cloudSpawnRate;
    private float spawnCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCount >= cloudSpawnRate)
        {
            GenerateCloud();
            spawnCount = 0;
        }
        else
        {
            spawnCount += Time.deltaTime;
        }
    }

    private void GenerateCloud(bool withFadeIn = true)
    {
        GameObject newCloud = Instantiate(cloud, basePosition, Quaternion.identity);
        CloudScript cloudScript = newCloud.GetComponent<CloudScript>();
        cloudScript.radius = Random.Range(8, 12);
        cloudScript.angle = Random.Range(0, 360);
        cloudScript.rotationSpeed = 6.5f + Random.Range(-0.5f, 0.5f);
        cloudScript.cloudMoveSpeed = 0.2f + Random.Range(-0.1f, 0.1f);

        if (!withFadeIn){
            SpriteRenderer sprite = newCloud.GetComponent<SpriteRenderer>();
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        }
    }
}
