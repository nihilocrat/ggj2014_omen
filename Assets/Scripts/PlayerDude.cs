using UnityEngine;
using System.Collections;

public class PlayerDude : MonoBehaviour
{
	public float exitRiseAmount = 10f;
	public float exitRiseTime = 3f;
	public GameObject grabTriggerPrefab;
	public Transform lastCheckPoint;
	public Transform grabLocation;
	public Transform holdLocation;
	public AudioClip[] deathSounds;
	public bool dead = false;

	private FullScreenFX fx;
	private CharacterController controller;

	private string firstLevelName = "level1";
	private float deathAltitude = -10f;

	void Awake()
	{
		controller = GetComponent<CharacterController>();

		fx = GameObject.FindObjectOfType<FullScreenFX>();
		if(Application.loadedLevelName == firstLevelName)
		{
			fx.SendMessage("OnGameBeginFX", 2f);
		}
		else
		{
			fx.SendMessage("OnLevelBeginFX", 2f);
		}
	}

	void Start()
	{
		if(lastCheckPoint == null)
		{
			lastCheckPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
		}

		// send me to the spawnpoint
		Respawn();
	}

	void Respawn()
	{
		dead = false;
		transform.position = lastCheckPoint.position;
		transform.rotation = lastCheckPoint.rotation;
	}

	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel("levelselect");
		}

		if(Input.GetButtonDown("ToggleMirroring"))
		{
			var mirror = FindObjectOfType<MirrorCamera>();
			mirror.doMirror = !mirror.doMirror;
		}

		if(Input.GetButtonDown("Action"))
		{
			// if I have grabbed something, drop it.
			if(holdLocation.childCount > 0)
			{
				var dropMe = holdLocation.GetChild(0);
				var dropGrabbable = dropMe.GetComponent<Grabbable>();
				dropGrabbable.enabled = true;
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

	void FixedUpdate()
	{
		// charactercontroller workaround
		if(controller.enabled)
		{
			controller.Move(Vector3.up * 0.001f);
		}

		if(transform.position.y <= deathAltitude)
		{
			SendMessage("OnPlayerDeath", gameObject);
		}
	}

	IEnumerator OnPlayerDeath(GameObject killer)
	{
		if(dead)
		{
			yield break;
		}
		dead = true;

		if(killer != null)
		{
			killer.SendMessage("OnKilledPlayer", SendMessageOptions.DontRequireReceiver);
		}
		/*
		transform.position = lastCheckPoint.position;
		transform.rotation = lastCheckPoint.rotation;
		*/

		var index = Random.Range(0, deathSounds.Length);
		audio.PlayOneShot(deathSounds[index]);

		OnDisable();

		var tilt = 1f;
		if(Random.value > 0.5f)
		{
			tilt *= -1f;
		}

		iTween.MoveAdd(gameObject, -Vector3.up * 0.5f, 1f);
		iTween.RotateAdd(gameObject, new Vector3(0f, 0f, 100f * tilt), 1f);

		fx.SendMessage("OnPlayerDeathFX", 1f);
		yield return new WaitForSeconds(1f);

		//Application.LoadLevel(Application.loadedLevel);
		Respawn();
		OnEnable();

		fx.SendMessage("OnPlayerRespawnFX", 1f);
		yield return new WaitForSeconds(1f);
	}

	IEnumerator OnPlayerWin(string nextLevelName)
	{
		if(dead)
		{
			yield break;
		}

		Debug.Log("Player won! Next level: " + nextLevelName);

		var highscores = FindObjectOfType<Highscores>();
		var timer = FindObjectOfType<Timer>();
		highscores.RecordScore(Application.loadedLevel, timer.time);

		OnDisable();
		dead = true; // technically not dead, but this prevents a lot of stuff bad stuff from happening

		iTween.MoveAdd(gameObject, Vector3.up * exitRiseAmount, exitRiseTime);

		fx.SendMessage("OnLevelEndFX", exitRiseTime);
		yield return new WaitForSeconds(exitRiseTime);

		if(string.IsNullOrEmpty(nextLevelName))
		{
			Application.LoadLevel(Application.loadedLevel + 1);
		}
		else
		{
			Application.LoadLevel(nextLevelName);
		}
	}

	void OnGrabSuccess(GameObject grabbed)
	{
		if(dead)
		{
			return;
		}

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
		if (body == null)
			return;

		// colliding with something kinematic that we want to fall apart
		if(hit.gameObject.CompareTag("Destructible") && body.isKinematic)
		{
			body.isKinematic = false;
			body.useGravity = true;
		}

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

	void OnDisable()
	{
		controller.enabled = false;

		GetComponent<MouseLook>().enabled = false;
		transform.Find("CameraHolder").GetComponent<MouseLook>().enabled = false;
	}

	void OnEnable()
	{
		controller.enabled = true;
		
		GetComponent<MouseLook>().enabled = true;
		transform.Find("CameraHolder").GetComponent<MouseLook>().enabled = true;
	}
}
