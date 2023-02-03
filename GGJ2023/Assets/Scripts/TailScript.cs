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

	// Small branches
	private int smallBranchCountdown = 1;
	[SerializeField] private GameObject smallBranch;
	[SerializeField] private int smallBranchSpacing = 10;
	
	[SerializeField]
	private List<GameObject> smallBranches;

	// This keeps the original tree which spawned this specific tail, in order to be removed with the tail.
	public GameObject originTree;

	private int fadeOutSteps = 20;
	private float fadeOutStepDuration = 0.3f; // In seconds
	
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
		if (Vector2.Distance(points.Last(), headTransform.position) > pointSpacing)
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
		points.Add(headTransform.position);
		line.positionCount = points.Count;
		line.SetPosition(points.Count - 1, headTransform.position);

		smallBranchCountdown--;
		if (smallBranchCountdown < 1) {
			GameObject newSmallBranch = Instantiate(smallBranch, headTransform.transform.position, 
					headTransform.transform.rotation * Quaternion.AngleAxis(Random.Range(0, 2) * 180, transform.forward));
			smallBranchCountdown = smallBranchSpacing / 2 +  Random.Range(1, smallBranchSpacing / 2);
			smallBranches.Add(newSmallBranch);
		}
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

	public IEnumerator FadeOutAndDestroy() {
		// So we don't collide with ourselves
		gameObject.GetComponent<EdgeCollider2D>().enabled = false;

		DisableDrawing();

		for (int i = 0; i < fadeOutSteps; i++)
		{
			Gradient fadeOutGradient = new Gradient();
        
			GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
			alphaKey[0].alpha = 0.0f;
			alphaKey[0].time = 0.0f;
			alphaKey[1].alpha = 1f - (1f/fadeOutSteps) * i;
			alphaKey[1].time = 1f - (1f/fadeOutSteps) * i;

			fadeOutGradient.alphaKeys = alphaKey;

			LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
			fadeOutGradient.colorKeys = lineRenderer.colorGradient.colorKeys;

			lineRenderer.colorGradient = fadeOutGradient;
			foreach (GameObject smallBranch in smallBranches)
			{
				smallBranch.gameObject.GetComponent<LineRenderer>().colorGradient = fadeOutGradient;
			}
			yield return new WaitForSeconds(fadeOutStepDuration);
		}

		foreach (GameObject smallBranch in smallBranches)
		{
			Destroy(smallBranch.gameObject);
		}

		Destroy(gameObject);

		if (originTree) {
			Destroy(originTree);
		}

	}
}
