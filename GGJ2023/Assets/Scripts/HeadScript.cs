using System.Collections;
using System;
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
    
	public float baseForwardSpeed;
    public float forwardSpeedIncreaseRate; // the higher the faster.
    public float totalForwardSpeed;
    private float forwardSpeedIncrease = 1;
    public float waterSpeedBoost = 1;
    [SerializeField]
    private float angularSpeed; 
    private EdgeCollider2D col;

    public MovementControls movementControls;

    private bool isTouched;

    // If set to true, can't collide with anything.
    private int gracePeriod = 0;

    private bool isInsideFactory = false;

    // Start is called before the first frame update
    void Start()
    {
        isTouched = false;
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

        if (!isTouched && horizontal == 0) {
            horizontal = UnityEngine.Random.Range(-0.3f, 0.3f);
        } else {
            isTouched = true;
        }
        forwardSpeedIncrease += forwardSpeedIncreaseRate * Time.deltaTime;
    }

    void FixedUpdate() {
        if (GetComponentInParent<PlayerScript>().isAlive) {
            totalForwardSpeed = baseForwardSpeed * waterSpeedBoost + Mathf.Log(forwardSpeedIncrease);
            transform.Translate(Vector2.up * totalForwardSpeed * Time.fixedDeltaTime, Space.Self);
            transform.Rotate(Vector3.forward * - horizontal * angularSpeed * Time.fixedDeltaTime);
        }
        else
        {
            totalForwardSpeed = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        AudioSource sound = other.gameObject.GetComponent<AudioSource>();
        if (sound) {
            sound.Play();
        }
        var playerScript = GetComponentInParent<PlayerScript>();
        if (other.tag == "Factory") {
            Debug.Log("HIT FACTORY?");
            FactoryScript factoryScript = other.gameObject.GetComponent<FactoryScript>();
            
            gracePeriod++;
            StartCoroutine("disableGracePeriod");
            playerScript.Respawn(other.gameObject);
            if (factoryScript) {
                StartCoroutine(factoryScript.turnIntoTree());
                playerScript.DestroyedFactory();
            }
        } else if (other.tag == "Collectable") {
            playerScript.Collect(other.gameObject);
        } else if (other.tag == "Obstacle") {
            Debug.Log("collision " + other.gameObject.name + " at x " + other.gameObject.transform.position.x);
            if (gracePeriod <= 0) {
                playerScript.Collision();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Planet") {
            Debug.Log("collision with planet");
            GetComponentInParent<PlayerScript>().Collision();
            // TODO: Spwan baby tree
        }
    }

    private IEnumerator disableGracePeriod() {
        yield return new WaitForSeconds(2f);
        gracePeriod--;
    }
}
