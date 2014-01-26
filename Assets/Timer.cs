using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
	public PlayerDude player;
	public TextMesh timerText;

	private float time = 0f;

	// Use this for initialization
	void Start ()
	{
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
			time = Time.time;
		}

		int min = Mathf.FloorToInt(time / 60f);
		int sec = Mathf.FloorToInt(time - min * 60f);

		timerText.text = string.Format("{0}:{1}", min, sec.ToString("00"));
	}
}
