using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    public float radius;
    public float angle;
    public float rotationSpeed = 6.5f;
    public float cloudMoveSpeed = 0.2f;
    public float fadeInRate = 0.1f;

    private float minRadius;
    private LogicManagerScript logicManager;

    // Start is called before the first frame update
    void Start()
    {
        logicManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
        minRadius = 0.6f * radius;
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite.color.a < 1) 
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a + fadeInRate * Time.deltaTime);
        }
        radius = Mathf.Max(Mathf.Lerp(radius, radius * getRadiusModifier(), Time.deltaTime * cloudMoveSpeed), minRadius);
        float radians = angle * Mathf.Deg2Rad;
        Vector3 newPosition = new Vector3(
            Mathf.Cos(radians) * radius, 
            Mathf.Sin(radians) * radius, 
            transform.position.z
        );
        transform.position = newPosition;
        transform.rotation = Quaternion.AngleAxis(angle - 90, transform.forward);
        
        angle += Time.deltaTime * rotationSpeed;
    }

    private float getRadiusModifier()
    {
        return 1f + (logicManager.factoryAmount > 3 ? (logicManager.factoryAmount - 3f) * -0.01f: 0.001f);
    }
}
