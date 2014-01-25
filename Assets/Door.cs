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

		Destroy(gameObject, animation.clip.length);
	}
}
