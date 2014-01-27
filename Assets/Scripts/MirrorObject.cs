using UnityEngine;
using System.Collections;

public class MirrorObject : MonoBehaviour
{
	/*
	public Transform normalVersion;
	public Transform mirroredVersion;
	*/

	private Quaternion originalRot;

	void Awake()
	{
		// NOTE : this needs to only be used on objects that aren't going to change their local rotation
		originalRot = transform.localRotation;
	}

	public void OnToggleMirroring(bool doMirror)
	{
		if(doMirror)
		{
			//transform.localRotation = originalRot;
			transform.Rotate(new Vector3(0f, -180f, 0f), Space.Self);
		}
		else
		{
			transform.Rotate(new Vector3(0f, 180f, 0f), Space.Self);
		}
	}
}
