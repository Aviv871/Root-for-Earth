using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TailScript : MonoBehaviour
{

    public float pointSpacing = 0.1f;
	private LineRenderer line;

	private List<Vector2> points;
	private EdgeCollider2D col;
	[SerializeField]
	private bool isDrawing = true;
	private HeadScript head;

	
	void Start()
	{
		line = GetComponent<LineRenderer>();
		col = GetComponent<EdgeCollider2D>();
		head = GetComponentInParent<HeadScript>();
		
		points = new List<Vector2>();
	}

	void Update()
	{
		if (points.Count <= 0)
		{
			SetPoint();
		}
		if (!line.enabled)
		{
			line.enabled = true;
		}
		if (!isDrawing)
		{
			return;
		}
		if (Vector2.Distance(points.Last(), head.transform.position) > pointSpacing)
		{
			SetPoint();
		}
	}

	void FixedUpdate() {
		
	}

	void SetPoint()
	{
		if (points.Count > 1)
		{
			col.points = points.ToArray<Vector2>();
		}
		points.Add(head.transform.position);
		line.positionCount = points.Count;
		line.SetPosition(points.Count - 1, head.transform.position);
	}

	// public void DisableDrawing()
	// {
	// 	isDrawing = false;
	// 	StartCoroutine(SynchronizeCollider());
	// }

	// IEnumerator SynchronizeCollider()
	// {
	// 	yield return new WaitForSeconds(0.2f);

	// 	if (points.Count > 1)
	// 	{
	// 		col.points = points.ToArray<Vector2>();
	// 	}
	// }
}
