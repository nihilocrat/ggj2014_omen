using UnityEngine;
using System.Collections;

public class PlayerDude : MonoBehaviour
{
	public Transform lastCheckPoint;

	void Start ()
	{
	
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
		killer.SendMessage("OnKilledPlayer");

		transform.position = lastCheckPoint.position;
		transform.rotation = lastCheckPoint.rotation;
	}
}
