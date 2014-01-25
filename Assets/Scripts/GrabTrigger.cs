using UnityEngine;
using System.Collections;

public class GrabTrigger : MonoBehaviour
{
	public GameObject owner;

	void Start()
	{
		Destroy(gameObject, 0.02f);
	}

	void OnTriggerEnter(Collider other)
	{
		other.gameObject.SendMessage("OnGrabbed", owner, SendMessageOptions.DontRequireReceiver);
	}
}
