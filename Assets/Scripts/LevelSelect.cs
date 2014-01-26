using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour
{
	public int startingLevelIndex = 3;
	public int numLevels = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI ()
	{
		DrawSelect(new Vector2(100f, 100f));
		DrawSelect(new Vector2(Screen.width - 500f, 100f));
	}

	void DrawSelect(Vector2 origin)
	{
		Vector2 size = new Vector2(400f, 500f);
		Vector2 buttonsize = new Vector2(100f, 100f);
		float textHeight = 24f;
		//GUI.Box(new Rect(origin.x - 50f, origin.y - 50f, size.x, size.y), "");//, style);
		
		for(int i=0; i < numLevels; i++)
		{
			int num = i + 1;
			int col = i % 3;
			int row = Mathf.FloorToInt(i / 3);

			if(GUI.Button(new Rect(origin.x + col * 100f, origin.y + row * 100f, buttonsize.x, buttonsize.y), num.ToString()))
			{
				InputNumber(num);
			}
		}
	}

	void InputNumber(int num)
	{
		Application.LoadLevel(Application.loadedLevel + num);
	}
}
