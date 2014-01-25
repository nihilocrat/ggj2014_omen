using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SignalSender))]
public class DropZone : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		var grabbable = other.GetComponent<Grabbable>();
		if(grabbable != null && grabbable.enabled)
		{
			OnDropped(grabbable);
		}
	}

	void OnDropped(Grabbable item)
	{
		// do stuff?!?!?
		Debug.Log("dropped " + item.name + " in dropzone : " + gameObject.name);
		GetComponent<SignalSender>().Send();
	}
}
