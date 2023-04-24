using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnd : MonoBehaviour
{
	[SerializeField] public PlayerMovement movement;
	[SerializeField] public GameObject heart;
	[SerializeField] private bool hasEnded = false;

    // Update is called once per frame
    void Update()
    {
		if (GameManager.instance.levelComplete && !hasEnded)
		{
			hasEnded = true;
			movement.enabled = false;
			//heart.SetActive(true);
		}
    }
}
