using UnityEngine;
using System.Collections;

public class PlayerDude : MonoBehaviour
{
	public Transform lastCheckPoint;

	void Start ()
	{
		if(lastCheckPoint == null)
		{
			lastCheckPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
		}

		// send me to the spawnpoint
		OnPlayerDeath(null);
	}

	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void OnPlayerDeath(GameObject killer)
	{
		if(killer != null)
		{
			killer.SendMessage("OnKilledPlayer");
		}

		transform.position = lastCheckPoint.position;
		transform.rotation = lastCheckPoint.rotation;
	}

	void OnPlayerWin(string nextLevelName)
	{
		if(string.IsNullOrEmpty(nextLevelName))
		{
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
}
