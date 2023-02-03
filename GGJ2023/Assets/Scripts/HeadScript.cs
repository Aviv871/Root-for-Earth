using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MovementControls 
{
  Arrows,
  ASDW
}

public class HeadScript : MonoBehaviour
{
	private float horizontal;
    
    [SerializeField]
	private float forwardSpeed; 
    [SerializeField]
	private float angularSpeed; 
    private EdgeCollider2D col;

    [SerializeField]
    private MovementControls movementControls;

    private LogicManagerScript logicManager;

    // Start is called before the first frame update
    void Start()
    {
        logicManager = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movementControls == MovementControls.ASDW) {
		    horizontal = Input.GetAxis("Horizontal AD");
        } else {
            horizontal = Input.GetAxis("Horizontal");
        }
    }

    void FixedUpdate() {
		transform.Translate(Vector2.up * forwardSpeed * Time.fixedDeltaTime, Space.Self);
        transform.Rotate(Vector3.forward * - horizontal * angularSpeed * Time.fixedDeltaTime);

    }

    void OnTriggerEnter2D(Collider2D other) {
        logicManager.GameOver();
    }
}
