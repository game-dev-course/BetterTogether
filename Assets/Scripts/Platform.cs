using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

	[SerializeField] public Sprite blue;
	[SerializeField] public Sprite pink;
	[SerializeField] public GameObject blueLight;
	[SerializeField] public GameObject pinkLight;
	[SerializeField] public LayerMask blueMask;
	[SerializeField] public LayerMask pinkMask;
	[SerializeField] public bool isStandable = true;
	[SerializeField] public bool isOutline = false;
	[SerializeField] public bool makeBlue;
	[SerializeField] public bool makePink;

	SpriteRenderer sr;
	PlatformEffector2D effector;

	private Sprite startSprite;
	private LayerMask startMask;

	private void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		effector = GetComponent<PlatformEffector2D>();

		startSprite = sr.sprite;
		if(effector != null)
			startMask = effector.colliderMask;

		if (makeBlue)
			MakeBlue();
		if (makePink)
			MakePink();
	}

	public void MakeBlue()
	{
		sr.sprite = blue;
		pinkLight.SetActive(false);
		blueLight.SetActive(true);
		gameObject.layer = LayerMask.NameToLayer("Blue");
		if (effector != null)
			effector.colliderMask = blueMask;
	}

	public void MakePink()
	{
		sr.sprite = pink;
		pinkLight.SetActive(true);
		blueLight.SetActive(false);
		gameObject.layer = LayerMask.NameToLayer("Pink");
		if (effector != null)
			effector.colliderMask = pinkMask;
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (isOutline)
			return;

		if (isStandable)
		{
			if (col.CompareTag("Player_BottomTrigger"))
			{
				string parentTag = col.transform.parent.tag;
				if (parentTag == "Player_Blue")
				{
					MakeBlue();
				}
				else if (parentTag == "Player_Pink")
				{
					MakePink();
				}
			}
		} else
		{
			if (col.CompareTag("Player_Blue"))
			{
				MakeBlue();
			}
			else if (col.CompareTag("Player_Pink"))
			{
				MakePink();
			}
		}
	}

}
