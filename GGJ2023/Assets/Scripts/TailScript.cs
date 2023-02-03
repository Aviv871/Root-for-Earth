using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TailScript : MonoBehaviour
{

    public float pointSpacing = 0.1f;
	private LineRenderer line;
    
	[SerializeField]
	private List<Vector2> points;
	private EdgeCollider2D col;
	[SerializeField]
	private bool isDrawing = true;
	private Transform headTransform;

	
	void Start()
	{
		line = GetComponent<LineRenderer>();
		col = GetComponent<EdgeCollider2D>();
		headTransform = GetComponentInParent<PlayerScript>().headTransform;
		
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
		// if (Vector2.Distance(points.Last(), headTransform.position) > pointSpacing)
		// {
		SetPoint();
		// }
	}

	void SetPoint()
	{
		if (points.Count > 1)
		{
			col.points = points.ToArray<Vector2>();
		}
		points.Add(headTransform.position);
		line.positionCount = points.Count;
		line.SetPosition(points.Count - 1, headTransform.position);
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
