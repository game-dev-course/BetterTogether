using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTimer : MonoBehaviour
{
	[SerializeField] public float changeInterval = 1f;
	[SerializeField] public bool isBlue = true;
	[SerializeField] public Sprite blue;
	[SerializeField] public Sprite pink;
	[SerializeField] public GameObject blueLight;
	[SerializeField] public GameObject pinkLight;
	[SerializeField] public LayerMask blueMask;
	[SerializeField] public LayerMask pinkMask;

	SpriteRenderer sr;
	PlatformEffector2D effector;

	float timeToChange;

	private void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		effector = GetComponent<PlatformEffector2D>();

		timeToChange = changeInterval;

		if (isBlue)
			MakeBlue();
		else
			MakePink();
	}

	private void Update()
	{
		if (Time.time >= timeToChange)
		{
			if (isBlue)
				MakePink();
			else
				MakeBlue();

			timeToChange = Time.time + changeInterval;
		}
	}

	void MakeBlue()
	{
		sr.sprite = blue;
		pinkLight.SetActive(false);
		blueLight.SetActive(true);
		gameObject.layer = LayerMask.NameToLayer("Blue");
		if (effector != null)
			effector.colliderMask = blueMask;
		isBlue = true;
	}

	void MakePink()
	{
		sr.sprite = pink;
		pinkLight.SetActive(true);
		blueLight.SetActive(false);
		gameObject.layer = LayerMask.NameToLayer("Pink");
		if (effector != null)
			effector.colliderMask = pinkMask;
		isBlue = false;
	}

}
