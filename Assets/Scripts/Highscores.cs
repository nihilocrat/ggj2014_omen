using UnityEngine;
using System.Collections.Generic;

public class LevelHighscore
{
	public List<Dictionary<string, int>> scores;
}

public class Highscores : MonoBehaviour
{
	private int maxScoresPerLevel = 3;
	private Dictionary<int, List<int>> scores;

	void Awake()
	{
		scores = new Dictionary<int, List<int>>();
		Load();
	}

	void Load()
	{
		var json = PlayerPrefs.GetString("Highscores", "");
		
		if(!string.IsNullOrEmpty(json))
		{
			scores = LitJson.JsonMapper.ToObject<Dictionary<int, List<int>>>(json);
		}
	}

	void Save()
	{
		string json = LitJson.JsonMapper.ToJson(scores);
		PlayerPrefs.SetString("Highscores", json);
	}

	void RecordScore(int levelNumber, float score)
	{
		int scoreInt = Mathf.FloorToInt(score);
		/*
		var levelScores = scores[levelNumber];
		foreach()
		{

		}
		*/
	}
}
