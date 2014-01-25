using UnityEngine;
using System.Collections;

public class PlayerDude : MonoBehaviour
{
	public GameObject grabTriggerPrefab;
	public Transform lastCheckPoint;
	public Transform grabLocation;

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

		if(Input.GetButtonDown("Action"))
		{
			// if I have grabbed something, drop it.
			if(grabLocation.childCount > 0)
			{
				var dropMe = grabLocation.GetChild(0);
			}
			// if not, try grabbing something.
			else
			{
				var obj = Instantiate(grabTriggerPrefab, grabLocation.position, grabLocation.rotation) as GameObject;
				obj.GetComponent<GrabTrigger>().owner = gameObject;
			}
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

	void OnGrabSuccess(GameObject grabbed)
	{
		grabbed.transform.parent = grabLocation;

		grabbed.transform.localPosition = Vector3.zero;
		grabbed.transform.localRotation = Quaternion.identity;
	}
}
