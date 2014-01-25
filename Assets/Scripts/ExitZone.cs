using UnityEngine;
using System.Collections;

public class ExitZone : MonoBehaviour
{
	public string nextLevelName;

	void OnTriggerEnter(Collider other)
	{
		other.gameObject.SendMessage("OnPlayerWin", nextLevelName);
	}
}
