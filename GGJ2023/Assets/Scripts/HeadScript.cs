using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadScript : MonoBehaviour
{
	private float horizontal;
    
    [SerializeField]
	private float forwardSpeed; 
    [SerializeField]
	private float angularSpeed; 
    private EdgeCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		horizontal = Input.GetAxis("Horizontal");
    }

    void FixedUpdate() {
		transform.Translate(Vector2.up * forwardSpeed * Time.fixedDeltaTime, Space.Self);
        transform.Rotate(Vector3.forward * - horizontal * angularSpeed * Time.fixedDeltaTime);

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Obstacle") {
            Debug.Log("Game Over");
        }
    }
}
