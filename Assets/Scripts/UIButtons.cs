using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIButtons : MonoBehaviour
{
	public GameObject MainMenu;
	public GameObject Settings;

	public Button restartButton, submitButton, quitButton, settingsButton;
	public Toggle postProcessToggle;
	public Toggle lowQualityToggle;
	public InputField nameInput;

	public HighScores HighScoreList;
	public GameObject ScoreTrigger;
	private Score Score;
	public GameObject Player;
	private Player player;
	public GameObject postProcessVolume;
	public bool hasSubmittedScore = false;

	private void Awake()
	{

		Score = ScoreTrigger.GetComponent<Score>();
		player = Player.GetComponent<Player>();
	}

	void Start()
	{
		//Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
		if (PlayerPrefs.GetString("postProcess") != "true")
		{
			postProcessVolume.SetActive(false);
			postProcessToggle.isOn = false;

		}
		if (PlayerPrefs.GetString("graphicsSetting") != "high")
		{
			QualitySettings.SetQualityLevel(0, true);
			lowQualityToggle.isOn = false;

		}
		else
		{
			QualitySettings.SetQualityLevel(1, true);

		}

		Button back = Settings.GetComponentInChildren<Button>();

		// string[] qualitySettings = QualitySettings.names;

		restartButton.onClick.AddListener(Restart);
		quitButton.onClick.AddListener(Quit);
		back.onClick.AddListener(ToggleSettings);
		settingsButton.onClick.AddListener(ToggleSettings);
		submitButton.onClick.AddListener(() => SubmitHighScore(nameInput.text));
		lowQualityToggle.onValueChanged.AddListener(delegate
		{
			ToggleQuality();
		});
		postProcessToggle.onValueChanged.AddListener(delegate
		{
			TogglePostProcess();
		});
		// m_YourSecondButton.onClick.AddListener(delegate { TaskWithParameters("Hello"); });
		// m_YourThirdButton.onClick.AddListener(() => ButtonClicked(42));
		// m_YourThirdButton.onClick.AddListener(TaskOnClick);
	}

	private void Update()
	{
		if (Input.GetButtonDown("Flap"))
		{
			if (player.isDead)
			{
				Restart();
			}
		}
		if (Input.GetButtonUp("Flap"))
		{
		}
		if (Input.GetKeyDown(KeyCode.Return))
		{
			SubmitHighScore(nameInput.text);
		}
	}

	void Restart()
	{
		SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

	}

	void SubmitHighScore(string name)
	{
		if (name != "" && !hasSubmittedScore)
		{
			HighScoreList.AddHighScore(Score.score / 2, name);
			nameInput.text = "";
			hasSubmittedScore = true;
		}

	}

	void Quit()
	{
		Application.Quit();
	}

	void ToggleSettings()
	{
		Settings.SetActive(!Settings.activeSelf);
		MainMenu.SetActive(!MainMenu.activeSelf);
	}

	void TogglePostProcess()
	{
		postProcessVolume.SetActive(!postProcessVolume.activeSelf);
		if (PlayerPrefs.GetString("postProcess") == "true")
			PlayerPrefs.SetString("postProcess", "false");
		else
			PlayerPrefs.SetString("postProcess", "true");
		PlayerPrefs.Save();
	}

	void ToggleQuality()
	{
		if (PlayerPrefs.GetString("graphicsSetting") == "high")
		{
			QualitySettings.SetQualityLevel(0, true);
			PlayerPrefs.SetString("graphicsSetting", "low");
		}
		else
		{
			PlayerPrefs.SetString("graphicsSetting", "high");
			QualitySettings.SetQualityLevel(1, true);
		}
		PlayerPrefs.Save();
	}


	void ButtonClicked(int buttonNo)
	{
		//Output this to console when the Button3 is clicked
		Debug.Log("Button clicked = " + buttonNo);
	}
}
