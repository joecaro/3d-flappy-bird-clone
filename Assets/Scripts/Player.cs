using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public World world;
	public GameObject start;
	public GameObject MainUI;
	public GameObject UIButtons;
	public GameObject restartButton;
	public GameObject ScoreListObject;
	public GameObject ScoreText;
	public GameObject ScoreTrigger;
	private Score Score;
	public AudioSource FlapAudio;
	public AudioSource HitAudio;
	public AudioSource MusicLoop;
	public ParticleSystem particleSystem;
	public GameObject cloud;





	[SerializeField]
	private float gravity = -1;

	[SerializeField]
	private float maxVerticalSpeed = 4;
	[SerializeField]



	private Quaternion myQuaternion = new Quaternion();

	private Vector3 nextPosition = new Vector3();



	private bool flap;
	public bool isDead = false;
	private Vector3 velocity = new Vector3(0, 0, 0);
	private float verticalMomentum = 0;



	private UIButtons uIButtons;
	Camera camera;

	private void Awake()
	{
		if (!PlayerPrefs.HasKey("settingsCreated"))
		{
			PlayerPrefs.SetString("settingsCreated", "true");
			PlayerPrefs.SetString("postProcess", "true");
			PlayerPrefs.SetString("graphicsSetting", "high");
			PlayerPrefs.Save();
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		camera = Camera.main;
		Score = ScoreTrigger.GetComponent<Score>();
		uIButtons = UIButtons.GetComponent<UIButtons>();
		InvokeRepeating("CreateCloud", 1f, 3f);


	}

	// Update is called once per frame
	private void FixedUpdate()
	{
		if (world.worldActive)
		{

			CalculateVelocity();
			if (flap && velocity.y < maxVerticalSpeed)
			{
				Flap();
			}
			checkBoundaries();


			transform.Translate(velocity, Space.World);
		}
		if (isDead)
		{
			CalculateVelocity();
			transform.Translate(velocity, Space.World);

		}
	}

	private void Update()
	{
		GetPlayerInputs();

		Vector3 movementDirection = new Vector3(15, velocity.y, 0);
		movementDirection.Normalize();

		nextPosition = transform.position + velocity * Time.deltaTime;

		myQuaternion.SetFromToRotation(transform.position, nextPosition);
		transform.rotation = myQuaternion * transform.rotation;
	}

	void checkBoundaries()
	{
		Vector2 screenPosition = camera.WorldToScreenPoint(transform.position + Vector3.right * 3);
		if (screenPosition.y > camera.pixelHeight)
		{
			if (flap)
				velocity.y = 0;
			else velocity.y = -0.1f;
		}
		if (screenPosition.y < 0)
		{
			Die();
		}

	}


	private void CalculateVelocity()
	{
		if (velocity.y > gravity)
			verticalMomentum += gravity * Time.fixedDeltaTime;


		// Apply vertical momentum (falling/jumping).
		velocity += Vector3.up * verticalMomentum * Time.fixedDeltaTime;

	}

	private void Flap()
	{
		velocity.y = maxVerticalSpeed;
		verticalMomentum = 0;
		flap = false;
		FlapAudio.Play();
	}

	private void StartGame()
	{
		world.worldActive = true;
		world.InvokeRepeatingInstances();
		start.SetActive(false);
		MainUI.SetActive(false);
		ScoreText.SetActive(true);
		MusicLoop.Play();
	}

	private void OnTriggerEnter(Collider other)
	{
		particleSystem.Emit(10);
		HitAudio.Play();
		Die();
	}

	private void Die()
	{
		world.worldActive = false;
		world.CancelRepeatedInstances();
		ScoreTrigger.SetActive(false);
		MusicLoop.Stop();


		velocity = new Vector3(-.5f, 1, -0.25f);
		verticalMomentum = -0.5f;


		isDead = true;
		restartButton.SetActive(true);
		uIButtons.hasSubmittedScore = false;
		ScoreListObject.SetActive(true);
	}

	void CreateCloud()
	{
		Instantiate(cloud);
	}


	private void GetPlayerInputs()
	{
		if (!isDead)
		{
			if (Input.touchCount > 0)
			{
				Touch touch = Input.GetTouch(0);

				// Handle finger movements based on touch phase.
				switch (touch.phase)
				{
					// Record initial touch position.
					case TouchPhase.Began:
						flap = true;
						if (!world.worldActive)
						{
							StartGame();
						}
						break;

					// Determine direction by comparing the current touch position with the initial one.
					case TouchPhase.Moved:
						break;

					// Report that a direction has been chosen when the finger is lifted.
					case TouchPhase.Ended:
						flap = false;
						break;
				}
			}

			if (Input.GetButtonDown("Flap"))
			{
				flap = true;
				if (!world.worldActive)
				{
					StartGame();
				}
			}
			if (Input.GetButtonUp("Flap"))
			{
				flap = false;
			}
		}
	}
}
