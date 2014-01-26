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
		scores = new Dictionary<int, List<int>>();

		for(int i=0; i < 10; i++)
		{
			var keyname = "Highscores_" + i.ToString();
			string json = PlayerPrefs.GetString(keyname, "");
			if(!string.IsNullOrEmpty(json))
			{
				scores[i] = new List<int>();
				scores[i] = LitJson.JsonMapper.ToObject<List<int>>(json);
				Debug.Log("LOADED: " + keyname + " : " + json);
			}
		}
	}

	void Save()
	{
		/*
		string json = LitJson.JsonMapper.ToJson(scores);
		PlayerPrefs.SetString("Highscores", json);
		*/
		foreach(KeyValuePair<int, List<int>> pair in scores)
		{
			var json = LitJson.JsonMapper.ToJson(pair.Value);
			var keyname = "Highscores_" + pair.Key.ToString();
			PlayerPrefs.SetString(keyname, json);
			Debug.Log("SAVED: " + keyname + " : " + json);
		}
	}

	public void RecordScore(int levelNumber, float score)
	{
		int scoreInt = Mathf.FloorToInt(score);
		
		if(!scores.ContainsKey(levelNumber))
		{
			scores[levelNumber] = new List<int>();
		}

		var levelScores = scores[levelNumber];
		levelScores.Add(scoreInt);
		levelScores.Sort();

		if(levelScores.Count > maxScoresPerLevel)
		{
			levelScores.RemoveAt(levelScores.Count-1);
		}

		Save();
	}

	public string GetScoresForLevel(int levelNumber)
	{
		var text = "Best:\n\n";

		if(!scores.ContainsKey(levelNumber))
		{
			return text;
		}

		var levelScores = scores[levelNumber];
		for(int i = 0; i < 3; i++)
		{
			if(i < levelScores.Count)
			{
				text += Timer.TimeToString(levelScores[i]) + "\n";
			}
      	}

		return text;
	}
}
