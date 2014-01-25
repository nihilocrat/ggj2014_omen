using UnityEngine;
using System.Collections;

public class Grabbable : MonoBehaviour
{
	void OnGrabbed(GameObject grabber)
	{
		grabber.SendMessage("OnGrabSuccess", gameObject);
	}
}
