using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	Camera camera;
	public float despawnPosX = 0;
	private Rigidbody rigid;
	private Vector3 velocity = new Vector3(-15, 0, 0);
	private Vector3 startPos = new Vector3(65, 0, 0);

	// Start is called before the first frame update
	void Start()
	{

		camera = Camera.main;
		var rd = new System.Random();

		int randomPos = rd.Next(-10, 10);

		rigid = GetComponent<Rigidbody>();
		rigid.velocity = velocity;
		rigid.transform.position = startPos + Vector3.up * randomPos;
	}

	private void Update()
	{
		Vector2 screenPosition = camera.WorldToScreenPoint(transform.position + Vector3.right * 24);
		if (screenPosition.x < despawnPosX)
			Destroy(gameObject);
		rigid.transform.position += velocity * Time.deltaTime;
	}


}
