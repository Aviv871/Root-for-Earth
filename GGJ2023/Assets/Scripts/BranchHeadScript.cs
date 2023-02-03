using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchHeadScript : MonoBehaviour
{
	private float horizontal;
    
	public float forwardSpeed; 
	private float angularSpeed; 
    private EdgeCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        angularSpeed = Random.Range(-80, 80);
        int horizontal_seed = Random.Range(0, 2);
        if (horizontal_seed == 1) {
            horizontal = 1;
        } else {
            horizontal = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (GetComponentInParent<BranchInstanceScript>().BranchTimeToLive % 200 == 0) {
        //     angularSpeed = Random.Range(-40, 40);
        // }
    }

    void FixedUpdate() {
        if (GetComponentInParent<BranchInstanceScript>().isGrowing) {
            transform.Translate(Vector2.up * forwardSpeed * Time.fixedDeltaTime, Space.Self);
            transform.Rotate(Vector3.forward * - horizontal * angularSpeed * Time.fixedDeltaTime);
        }
    }
}
