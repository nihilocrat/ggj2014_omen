using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
	void OnOpen()
	{
		if(animation)
		{
			animation.Play();
		}

		Destroy(this, animation.clip.length);
		Destroy(transform.Find("Hinge").gameObject, animation.clip.length);

	}
}
