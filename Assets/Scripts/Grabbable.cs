using UnityEngine;
using System.Collections;

public class Grabbable : MonoBehaviour
{
	void OnGrabbed(GameObject grabber)
	{
		this.enabled = false;
		grabber.SendMessage("OnGrabSuccess", gameObject);
	}
}
