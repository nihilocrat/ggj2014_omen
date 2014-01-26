using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
	public PlayerDude player;
	public TextMesh timerText;

	public float time = 0f;
	private float levelBeginTime = 0f;

	// Use this for initialization
	void Start ()
	{
		levelBeginTime = Time.time;

		if(player == null)
		{
			player = FindObjectOfType<PlayerDude>();
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		if(!player.dead)
		{
			time = Time.time - levelBeginTime;
		}

		timerText.text = TimeToString(time);
	}

	static public string TimeToString(float inputTime)
	{
		int min = Mathf.FloorToInt(inputTime / 60f);
		int sec = Mathf.FloorToInt(inputTime - min * 60f);
		
		return string.Format("{0}:{1}", min, sec.ToString("00"));
	}
}
