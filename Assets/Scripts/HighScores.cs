using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScores : MonoBehaviour
{


	public Transform entryContainer;
	public Transform entryTemplate;
	private float templateHeight = 90f;


	private List<HighScoreEntry> highScoreEntryList;
	private List<Transform> highScoreEntryTransformList = new List<Transform>();
	private HighScoresList highScoresList;
	private void Awake()
	{
		if (PlayerPrefs.GetString("HasScoreData") == "")
		{

			highScoreEntryList = new List<HighScoreEntry>
		{
			new HighScoreEntry{ score = 0, name = "AAA"},
			new HighScoreEntry{ score = 0, name = "AAA"},
			new HighScoreEntry{ score = 0, name = "AAA"},
			new HighScoreEntry{ score = 0, name = "AAA"},
			new HighScoreEntry{ score = 0, name = "AAA"},
			new HighScoreEntry{ score = 0, name = "AAA"},
		};
			highScoresList = new HighScoresList { highScoreEntryList = highScoreEntryList };
			string json = JsonUtility.ToJson(highScoresList);
			PlayerPrefs.SetString("highscoreTable", json);
			PlayerPrefs.SetString("HasScoreData", "True");
			PlayerPrefs.Save();
		}

		string jsonString = PlayerPrefs.GetString("highscoreTable");
		highScoresList = JsonUtility.FromJson<HighScoresList>(jsonString);
		highScoreEntryList = highScoresList.highScoreEntryList;

		SortList();
		ChopTo5Entries();
		GenrateHighScoreList();

		entryTemplate.gameObject.SetActive(false);



	}

	public void AddHighScore(int score, string name)
	{
		foreach (Transform child in entryContainer)
		{
			GameObject.Destroy(child.gameObject);
		}
		highScoreEntryList.Add(new HighScoreEntry { score = score, name = name });
		SortList();
		ChopTo5Entries();
		highScoresList.highScoreEntryList = highScoreEntryList;
		string json = JsonUtility.ToJson(highScoresList);
		PlayerPrefs.SetString("highscoreTable", json);
		PlayerPrefs.Save();
		GenrateHighScoreList();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void SortList()
	{
		for (int i = 0; i < highScoreEntryList.Count; i++)
		{
			for (int j = i + 1; j < highScoreEntryList.Count; j++)
			{
				if (highScoreEntryList[j].score > highScoreEntryList[i].score)
				{
					// SWAP
					HighScoreEntry temp = highScoreEntryList[i];
					highScoreEntryList[i] = highScoreEntryList[j];
					highScoreEntryList[j] = temp;
				}

			}
		}
	}

	private void GenrateHighScoreList()
	{
		int currentEntry = 1;
		foreach (HighScoreEntry highScoreEntry in highScoreEntryList)
		{
			CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highScoreEntryTransformList, currentEntry);
			currentEntry++;
		}
	}

	private void ChopTo5Entries()
	{
		List<HighScoreEntry> tempList = highScoreEntryList.GetRange(0, 5);
		highScoreEntryList = tempList;
	}

	private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList, int currentEntry)
	{
		Transform entryTransform = Instantiate(entryTemplate, container);
		RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
		entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
		entryTransform.gameObject.SetActive(true);

		int score = highScoreEntry.score;
		string name = highScoreEntry.name;

		entryTransform.Find("Score").GetComponent<Text>().text = score.ToString();
		entryTransform.Find("Name").GetComponent<Text>().text = name;

		entryTransform.SetParent(entryContainer, false);
		entryRectTransform.localPosition = new Vector3(0, -1 * (templateHeight * currentEntry + 43), 0);

	}

	private class HighScoresList
	{
		public List<HighScoreEntry> highScoreEntryList;
	}

	[System.Serializable]
	private class HighScoreEntry
	{
		public int score;
		public string name;
	}
}

