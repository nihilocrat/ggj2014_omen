using UnityEngine;
using System.Collections;

public class Keypad : MonoBehaviour
{
	public bool numericInput = true;
	public string[] possiblePasswords;
	
	private string currentCode = ":) :) :)";
	private string targetCode = "1234";
	private bool showKeypad = false;

	private GameObject player;

	void Start()
	{
		targetCode = CreateCode();
		if(!numericInput)
		{
			currentCode = "";
		}
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
		GUIStyle style = new GUIStyle(GUI.skin.box);
		//style.font = font;
		//GUI.color = Color.white;
		style.fontSize *= 2;

		//Vector2 origin = new Vector2(100f, 100f);
		Vector2 size = new Vector2(400f, 500f);
		Vector2 buttonsize = new Vector2(100f, 100f);
		float textHeight = 24f;
		GUI.Box(new Rect(origin.x - 50f, origin.y - 50f, size.x, size.y), "ENTER ACCESS CODE");//, style);
		
		GUI.Label(new Rect(origin.x + (size.x/3), origin.y - 30f, size.x, textHeight), currentCode);//, style);

		if(isGod)
		{
			GUI.Label(new Rect(origin.x, origin.y + 40f, size.x, textHeight), "THE CORRECT CODE IS:    " + targetCode);//, style);
		}
		else
		{
			if(numericInput)
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
			else
			{
				Event e = Event.current;

				GUI.SetNextControlName("KeypadInput");
				currentCode = GUI.TextField(new Rect(origin.x, origin.y + 40f, size.x/2, textHeight), currentCode, 40);//, style);

				if (GUI.GetNameOfFocusedControl() == string.Empty)
				{
					GUI.FocusControl("KeypadInput");
				}

				if(GUI.Button(new Rect(origin.x, origin.y + 80f, buttonsize.x, buttonsize.y), "ENTER")
				   || e.keyCode == KeyCode.Return || Input.GetKeyDown(KeyCode.Return))
				{
					ValidateCode();
				}
				
				if(GUI.Button(new Rect(origin.x + buttonsize.x, origin.y + 80f, buttonsize.x, buttonsize.y), "X"))
				{
					currentCode = "";
					CloseKeypad();
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
		if(numericInput)
		{
			int codeNum = Random.Range(1001, 9999);
			return codeNum.ToString();
		}
		else
		{
			int index = Random.Range(0, possiblePasswords.Length);
			return possiblePasswords[index];
		}
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
			ValidateCode();
		}
	}

	void ValidateCode()
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
			currentCode = "INVALID! >:(";
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
