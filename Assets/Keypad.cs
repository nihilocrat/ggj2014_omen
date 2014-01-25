using UnityEngine;
using System.Collections;

public class Keypad : MonoBehaviour
{	
	private string currentCode = ":) :) :)";
	private string targetCode = "1234";
	private bool showKeypad = false;

	private GameObject player;

	void Start()
	{
		targetCode = CreateCode();
	}

	void OnGrabbed(GameObject grabber)
	{
		if(currentCode == targetCode)
		{
			return;
		}

		showKeypad = true;
		player = grabber;
		player.SendMessage("OnDisable");
	}

	void DrawKeyPad(Vector2 origin, bool isGod)
	{
		//Vector2 origin = new Vector2(100f, 100f);
		Vector2 size = new Vector2(400f, 500f);
		Vector2 buttonsize = new Vector2(100f, 100f);
		GUI.Box(new Rect(origin.x - 50f, origin.y - 50f, size.x, size.y), "ENTER ACCESS CODE");
		
		GUI.Label(new Rect(origin.x + (size.x/3), origin.y - 30f, size.x, 20f), currentCode);

		if(isGod)
		{
			GUI.Label(new Rect(origin.x, origin.y + 40f, size.x, 20f), "THE CORRECT CODE IS:    " + targetCode);
		}
		else
		{
			for(int i=0; i < 10; i++)
			{
				int num = i + 1;
				int col = i % 3;
				int row = Mathf.FloorToInt(i / 3);
				
				if(i != 9)
				{
					if(GUI.Button(new Rect(origin.x + col * 100f, origin.y + row * 100f, buttonsize.x, buttonsize.y), num.ToString()))
					{
						InputNumber(num);
					}
				}
				// special case for "0"
				else
				{
					col = 1;
					row = 3;
					
					if(GUI.Button(new Rect(origin.x + col * 100f, origin.y + row * 100f, buttonsize.x, buttonsize.y), "0"))
					{
						InputNumber(0);
					}

					// exit key
					col += 1;
					if(GUI.Button(new Rect(origin.x + col * 100f, origin.y + row * 100f, buttonsize.x, buttonsize.y), "X"))
					{
						currentCode = ":) :) :)";
						CloseKeypad();
					}

					break;
				}
			}
		}
	}

	void OnGUI()
	{
		if(!showKeypad)
		{
			return;
		}

		DrawKeyPad(new Vector2(100f, 100f), false);
		DrawKeyPad(new Vector2(Screen.width - 500f, 100f), true);
	}

	string CreateCode()
	{
		int codeNum = Random.Range(1001, 9999);
		return codeNum.ToString();
	}

	void InputNumber(int num)
	{
		// code is in the startup or invalid state
		if(currentCode.Length > targetCode.Length)
		{
			currentCode = "";
		}

		currentCode += num.ToString();

		if(currentCode.Length >= targetCode.Length)
		{
			if(currentCode == targetCode)
			{
				// success!
				Debug.Log("KEYPAD: success!");
				OnKeypadSuccess();
			}
			else
			{
				Debug.Log("KEYPAD: failure!");
				currentCode += " INVALID CODE! >:(";
			}
		}
	}

	void OnKeypadSuccess()
	{
		GetComponent<SignalSender>().Send();
		CloseKeypad();
	}

	void CloseKeypad()
	{
		player.SendMessage("OnEnable");
		showKeypad = false;
	}
}
