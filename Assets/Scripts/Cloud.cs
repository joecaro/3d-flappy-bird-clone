using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
	Camera camera;
	public float despawnPosX = 0;
	private Rigidbody rigid;
	private Vector3 velocity = new Vector3(-15, 0, 0);
	private Vector3 startPos = new Vector3(90, 0, 0);

	// Start is called before the first frame update
	void Start()
	{

		camera = Camera.main;
		var rd = new System.Random();

		int randomPosVertical = rd.Next(10, 50);
		int randomPosHorizontal = rd.Next(10, 50);

		rigid = GetComponent<Rigidbody>();
		rigid.velocity = velocity;
		rigid.transform.position = startPos + Vector3.up * randomPosVertical + Vector3.back * randomPosHorizontal * -1;

		velocity.x += randomPosHorizontal / 5;
	}

	private void Update()
	{
		Vector2 screenPosition = camera.WorldToScreenPoint(transform.position + Vector3.right * 24);
		if (screenPosition.x < despawnPosX)
			Destroy(gameObject);
		rigid.transform.position += velocity * Time.deltaTime;
	}

}
