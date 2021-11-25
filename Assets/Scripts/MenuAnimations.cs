using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimations : MonoBehaviour
{
	private GameObject spaceUp;
	// Start is called before the first frame update
	void Start()
	{
		spaceUp = GameObject.Find("SpaceUpEmpty");
		InvokeRepeating("SwitchActiveState", 0f, 1f);

	}

	// Update is called once per frame
	private void SwitchActiveState()
	{
		spaceUp.SetActive(!spaceUp.activeSelf);
	}
}
