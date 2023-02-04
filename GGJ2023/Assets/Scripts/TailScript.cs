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

	private float fadeOutSeconds = 1;
	private float timerFromSpawn = 0;
	private bool isColliderStillDisabled = true;

	private float timer = 0;
	private bool isFadingOut = false;

	void Start()
	{
		line = GetComponent<LineRenderer>();
		col = GetComponent<EdgeCollider2D>();
		headTransform = GetComponentInParent<PlayerScript>().headTransform;
		
		points = new List<Vector2>();
	}

	void Update()
	{
		if (isColliderStillDisabled) {
			timerFromSpawn += Time.deltaTime;
			// Wait one second before you go-go-go-go-go!!!!!!
			// (enabling the head to move away from a new tail before it's caught in the new collider)
			if (timerFromSpawn > 1) {
				col.enabled = true;
				isColliderStillDisabled = false;
			}
		}

		FadeOutFrame();

		if (!GetComponentInParent<PlayerScript>().isAlive) {
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
		if (isDrawing && Vector2.Distance(points.Last(), headTransform.position) > pointSpacing)
		{
			SetPoint();
		}
	}

	void SetPoint()
	{
		if (points.Count > 1) // TODO: maybe move this to end of function
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
			Color thisColor = GetComponentInParent<PlayerScript>().color;
			newSmallBranch.GetComponentInChildren<BranchHeadScript>().gameObject.GetComponent<SpriteRenderer>().color = thisColor;
            newSmallBranch.GetComponentInChildren<BranchTailScript>().gameObject.GetComponent<Renderer>().material.color = thisColor;
            smallBranchCountdown = smallBranchSpacing / 2 +  Random.Range(1, smallBranchSpacing / 2);
			smallBranches.Add(newSmallBranch);
		}
	}

	public void DisableDrawing()
	{
		isDrawing = false;
	}

	public void BeginFadingOut() {
		timer = 0;
		isFadingOut = true;
	}

	private void FadeOutFrame() {
		if (!isFadingOut) {
			return;
		}

		timer += Time.deltaTime;

		if (timer >= fadeOutSeconds) {
			isFadingOut = false;

			foreach (GameObject smallBranch in smallBranches)
			{
				Destroy(smallBranch.gameObject);
			}

			if (originTree) {
				Destroy(originTree);
			}

			Debug.Log("Destroying a tail!" + gameObject.GetInstanceID());
			Destroy(gameObject);
		}

		float fadingPortion = timer / fadeOutSeconds;
		LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();

		Gradient fadeOutGradient = GetLineGradientFadeOut(lineRenderer, fadingPortion);
		lineRenderer.colorGradient = fadeOutGradient;


		// For the small branches, we want the process faster
		float fadingPortionForSmallBranches = fadingPortion * 1.4f;
		if (fadingPortionForSmallBranches <= 1) {
			foreach (GameObject smallBranch in smallBranches) {
				// Fade out the small branch heads
				GameObject smallHead = smallBranch.gameObject.GetComponentInChildren<BranchHeadScript>().gameObject;
				SpriteRenderer smallHeadSprite = smallHead.GetComponent<SpriteRenderer>();
				smallHeadSprite.color = new Color(smallHeadSprite.color.r, smallHeadSprite.color.g, smallHeadSprite.color.b,
					1f - fadingPortionForSmallBranches);

				// Fade out the small branch tails
				fadeOutGradient = GetLineGradientFadeOut(lineRenderer, fadingPortionForSmallBranches, true);
				smallBranch.gameObject.GetComponentInChildren<LineRenderer>().colorGradient = fadeOutGradient;
			}
		}

	}

  private Gradient GetLineGradientFadeOut(LineRenderer lineRenderer, float portion, bool reverseDirection = false)
  {
		Gradient fadeOutGradient = new Gradient();
					
		GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
		alphaKey[0].alpha = 1.0f - (portion) / 2; // Just to make the gradient less harsh torwards the end
		alphaKey[0].time = 0.0f;
		alphaKey[1].alpha = 1f - portion;
		alphaKey[1].time = 1f - portion;

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
