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

	private int fadeOutSteps = 100;
	private float fadeOutStepDuration = 0.05f; // In seconds
	
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
	}

	public IEnumerator FadeOutAndDestroy() {
		// Old tail shouldn't follow the new head
		DisableDrawing();

		for (int i = 0; i < fadeOutSteps; i++)
		{

			LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
			Gradient fadeOutGradient = GetLineGradientFadeOut(lineRenderer, i, fadeOutSteps);
			lineRenderer.colorGradient = fadeOutGradient;

			// For the small branches, we want the process faster
			int fadeOutStepsForSmallBranches = Mathf.RoundToInt(fadeOutSteps / 1.4f);
			if (i <= fadeOutStepsForSmallBranches) {
				foreach (GameObject smallBranch in smallBranches)
				{
					// Fade out the small branch heads
					GameObject smallHead = smallBranch.gameObject.GetComponentInChildren<BranchHeadScript>().gameObject;
					SpriteRenderer smallHeadSprite = smallHead.GetComponent<SpriteRenderer>();
					smallHeadSprite.color = new Color(smallHeadSprite.color.r, smallHeadSprite.color.g, smallHeadSprite.color.b,
						1f - (1f/fadeOutStepsForSmallBranches) * i);

					// Fade out the small branch tails
					fadeOutGradient = GetLineGradientFadeOut(lineRenderer, i, fadeOutStepsForSmallBranches, true);
					smallBranch.gameObject.GetComponentInChildren<LineRenderer>().colorGradient = fadeOutGradient;
				}
			}

			yield return new WaitForSeconds(fadeOutStepDuration);
		}

		foreach (GameObject smallBranch in smallBranches)
		{
			Destroy(smallBranch.gameObject);
		}


		if (originTree) {
			Destroy(originTree);
		}

		Debug.Log("Destroying a tail!");
		Destroy(gameObject);
	}

  private Gradient GetLineGradientFadeOut(LineRenderer lineRenderer, int step, int totalSteps, bool reverseDirection = false)
  {
    Gradient fadeOutGradient = new Gradient();
        
	GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
	alphaKey[0].alpha = 1.0f - ((1f/totalSteps) * step) / 2; // Just to make the gradient less harsh torwards the end
	alphaKey[0].time = 0.0f;
	alphaKey[1].alpha = 1f - (1f/totalSteps) * step;
	alphaKey[1].time = 1f - (1f/totalSteps) * step;

	// if both points are in the same place, it causes weird stuff
	if (alphaKey[1].time == 0) {
		alphaKey[0].alpha = 0;
	}

	if (reverseDirection) {
		alphaKey.Reverse();
	}

	fadeOutGradient.alphaKeys = alphaKey;

	fadeOutGradient.colorKeys = lineRenderer.colorGradient.colorKeys;

	return fadeOutGradient;
  }
}
