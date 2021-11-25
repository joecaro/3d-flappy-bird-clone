using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{

	private bool _worldActive = false;
	public bool worldActive
	{
		get
		{
			return _worldActive;
		}
		set
		{
			_worldActive = value;
		}
	}
	public GameObject pipes;
	// Start is called before the first frame update
	void Start()
	{

	}

	public void InvokeRepeatingInstances()
	{
		InvokeRepeating("CreateObstacle", 0.5f, 4f);
	}

	void CreateObstacle()
	{
		Instantiate(pipes);
	}

	public void CancelRepeatedInstances()
	{
		CancelInvoke();
	}
}
