using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject tailPrefab;
	public List<Tail> tails;
	public Transform headTransform;

	public int currentTailIndex = 0;
	public float enableTailDelay = 3f;
	public bool isImmune = true;

	private Color tailColor;
	private Vector3 startHeadPosition;
	private bool makingGap = false;
	private bool isBeingInvoked = false;

	void Start()
	{
		headTransform = GetComponentInChildren<Head>().gameObject.transform;
		tails = new List<Tail>();
		Tail firstTail = GetComponentInChildren<Tail>(true);
		tails.Add(firstTail);
		tailColor = GameManager.Instance.GetTailColor();
		firstTail.gameObject.SetActive(false);
		Invoke("EnableFirstTail", enableTailDelay);
	}

	void Update()
	{
		if (headTransform == null || isImmune)
		{
			return;
		}
		if (!isBeingInvoked)
		{
			Invoke("MakeGap", Random.Range(Settings.GapTimeoutMin, Settings.GapTimeoutMax));
			isBeingInvoked = true;
		}
		if (Vector3.Distance(startHeadPosition, headTransform.position) >= Settings.GapSize && makingGap)
		{
			CloseGap();
		}
	}

	void MakeGap()
	{
		makingGap = true;
		if (headTransform != null)
		{
			startHeadPosition = headTransform.position;
			tails[currentTailIndex].DisableDrawing();
		}
		else
		{
			makingGap = false;
		}
	}

	void CloseGap()
	{
		GameObject newTail = Instantiate(tailPrefab, Vector3.zero, Quaternion.identity);
		newTail.transform.parent = transform;
		tails.Add(newTail.GetComponent<Tail>());
		currentTailIndex++;
		SetTailColor();
		makingGap = false;
		isBeingInvoked = false;
	}

	void SetTailColor()
	{
		tails[currentTailIndex].line.startColor = tailColor;
		tails[currentTailIndex].line.endColor = tailColor;
	}

	void EnableFirstTail()
	{
		tails[currentTailIndex].gameObject.SetActive(true);
		isImmune = false;
		SetTailColor();
	}
}
