using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
	void Awake()
	{
		// visuals are editor-only
		Destroy(renderer);
		Destroy(GetComponent<MeshFilter>());
	}

	void OnTriggerEnter(Collider other)
	{
		var dude = other.GetComponent<PlayerDude>();
		if(dude != null)
		{
			Debug.Log("Checkpoint reached: " + gameObject.name);
			dude.lastCheckPoint = transform;
		}

		// prevent checkpoints from being re-triggered in case they backtrack for some reason
		collider.enabled = false;
	}
}
