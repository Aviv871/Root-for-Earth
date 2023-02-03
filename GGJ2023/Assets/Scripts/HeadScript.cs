using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementControls 
{
  Arrows,
  AS,
  BN,
  OP,
}

public class HeadScript : MonoBehaviour
{
	private float horizontal;
    
	public float forwardSpeed; 
    [SerializeField]
	private float angularSpeed; 
    private EdgeCollider2D col;

    public MovementControls movementControls;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movementControls == MovementControls.Arrows) {
            horizontal = Input.GetAxis("Horizontal");
        }
        else if (movementControls == MovementControls.AS) {
		    horizontal = Input.GetAxis("Horizontal AS");
        }
        else if (movementControls == MovementControls.BN) {
            horizontal = Input.GetAxis("Horizontal BN");
        }
        else if (movementControls == MovementControls.OP) {
            horizontal = Input.GetAxis("Horizontal OP");
        } else {
            Debug.Log("Failed to determine movement controls set");
        }
    }

    void FixedUpdate() {
        if (GetComponentInParent<PlayerScript>().isAlive) {
            transform.Translate(Vector2.up * forwardSpeed * Time.fixedDeltaTime, Space.Self);
            transform.Rotate(Vector3.forward * - horizontal * angularSpeed * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        AudioSource sound = other.gameObject.GetComponent<AudioSource>();
        if (sound) {
            sound.Play();
        }

        if (other.tag == "Factory") {
            // Destroy(other.gameObject);
            FactoryScript factoryScript = other.gameObject.GetComponent<FactoryScript>();
            if (factoryScript) {
                StartCoroutine(factoryScript.turnIntoTree());
            }
            GetComponentInParent<PlayerScript>().Respawn();
        } else if (other.tag == "Collectable") {
            GetComponentInParent<PlayerScript>().Collect(other.gameObject);
        } else if (other.tag == "Obstacle") {
            GetComponentInParent<PlayerScript>().Collision();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Planet") {
            GetComponentInParent<PlayerScript>().Collision();
            // TODO: Spwan baby tree
        }
    }
}
