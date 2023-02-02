using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class Tail : MonoBehaviour
{
	public float pointSpacing = 0.1f;
	public LineRenderer line;

	private List<Vector2> points;
	private EdgeCollider2D col;
	[SerializeField]
	private bool isDrawing = true;
	[SerializeField]
	private Transform head;

	void Start()
	{
		line = GetComponent<LineRenderer>();
		col = GetComponent<EdgeCollider2D>();
		head = GetComponentInParent<Player>().headTransform;

		points = new List<Vector2>();
	}

	void Update()
	{
		if (head == null)
		{
			head = GetComponentInParent<Player>().headTransform;
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
		if (!isDrawing)
		{
			return;
		}
		if (Vector3.Distance(points.Last(), head.position) > pointSpacing)
		{
			SetPoint();
		}
	}

	void SetPoint()
	{
		if (points.Count > 1)
		{
			col.points = points.ToArray<Vector2>();
		}
		points.Add(head.position);
		line.positionCount = points.Count;
		line.SetPosition(points.Count - 1, head.position);
	}

	public void DisableDrawing()
	{
		isDrawing = false;
		StartCoroutine(SynchronizeCollider());
	}

	IEnumerator SynchronizeCollider()
	{
		yield return new WaitForSeconds(0.2f);

		if (points.Count > 1)
		{
			col.points = points.ToArray<Vector2>();
		}
	}
}
