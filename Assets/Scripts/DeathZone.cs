using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
	public bool destroyWhenTriggered = false;

	void OnTriggerEnter(Collider other)
	{
		other.gameObject.SendMessage("OnPlayerDeath", gameObject, SendMessageOptions.DontRequireReceiver);
	}

	void OnCollisionEnter(Collision c)
	{
		c.gameObject.SendMessage("OnPlayerDeath", gameObject, SendMessageOptions.DontRequireReceiver);
	}

	void OnKilledPlayer()
	{
		if(destroyWhenTriggered)
		{
			Destroy(gameObject);
		}
	}
}
