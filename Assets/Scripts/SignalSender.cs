using UnityEngine;
using System.Collections;

public class SignalSender : MonoBehaviour
{
	public Transform recipient;
	public string messageWhenTriggered;
	public string messageArgument;	

	public void Send()
	{
		SendSignal(recipient, messageWhenTriggered, messageArgument);
	}

	static public void SendSignal(Transform recipient, string messageWhenTriggered, string messageArgument)
	{
		if(string.IsNullOrEmpty(messageArgument))
		{
			recipient.BroadcastMessage(messageWhenTriggered, messageArgument);
		}
		else
		{
			recipient.BroadcastMessage(messageWhenTriggered);
		}
	}
}
