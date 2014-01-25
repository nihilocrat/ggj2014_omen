using UnityEngine;
using System.Collections;

public class PlayerDude : MonoBehaviour
{
	public GameObject grabTriggerPrefab;
	public Transform lastCheckPoint;
	public Transform grabLocation;
	public Transform holdLocation;

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
			if(holdLocation.childCount > 0)
			{
				var dropMe = holdLocation.GetChild(0);
				dropMe.parent = null;

				dropMe.rigidbody.useGravity = true;
				dropMe.rigidbody.isKinematic = false;
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
		grabbed.transform.parent = holdLocation;

		grabbed.transform.localPosition = Vector3.zero;
		grabbed.transform.localRotation = Quaternion.identity;
		
		grabbed.rigidbody.useGravity = false;
		grabbed.rigidbody.isKinematic = true;
	}

	// this script pushes all rigidbodies that the character touches
	private  float pushPower = 2.0f;
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		var body = hit.collider.attachedRigidbody;
		// no rigidbody
		if (body == null || body.isKinematic)
			return;
		
		// We dont want to push objects below us
		if (hit.moveDirection.y < -0.3f) 
			return;
		
		// Calculate push direction from move direction, 
		// we only push objects to the sides never up and down
		var pushDir = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);
		// If you know how fast your character is trying to move,
		// then you can also multiply the push velocity by that.
		
		// Apply the push
		body.velocity = pushDir * pushPower;
	}
}
