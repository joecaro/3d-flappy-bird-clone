using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
	public int score = 0;
	public Text scoreText;
	public AudioSource scoreSound;


	// Update is called once per frame
	void Update()
	{
		scoreText.text = "Score: " + Mathf.Ceil(score / 2);

	}

	private void OnTriggerEnter(Collider other)
	{
		score++;
		scoreSound.Play();
	}
}
