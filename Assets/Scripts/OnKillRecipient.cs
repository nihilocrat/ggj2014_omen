using UnityEngine;
using System.Collections;

public class OnKillRecipient : MonoBehaviour
{
	void OnKill()
	{
		Destroy(gameObject);
	}
}
