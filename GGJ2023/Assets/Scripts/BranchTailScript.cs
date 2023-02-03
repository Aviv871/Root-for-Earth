using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BranchTailScript : MonoBehaviour
{

	public float pointSpacing = 0.1f;
    private LineRenderer line;
    
	[SerializeField]
	private List<Vector2> points;
	[SerializeField]
	public bool isDrawing = true;
	private Transform headTransform;
	
	void Start()
	{
		line = GetComponent<LineRenderer>();
		headTransform = GetComponentInParent<BranchInstanceScript>().headTransform;
		
		points = new List<Vector2>();
	}

	void Update()
	{
		if (!isDrawing)
		{
			return;
		}
        if (points.Count <= 0)
		{
			SetPoint();
		}
		if (!line.enabled)
		{
			line.enabled = true;
		}
		if (Vector2.Distance(points.Last(), headTransform.position) > pointSpacing)
		{
			SetPoint();
		}
	}

	void SetPoint()
	{
		points.Add(headTransform.position);
		line.positionCount = points.Count;
		line.SetPosition(points.Count - 1, headTransform.position);
		
	}

	public void DisableDrawing()
	{
		isDrawing = false;
	}
}
