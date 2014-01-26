using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour
{
	public string[] levelNames;
	private FullScreenFX fx;
	private Highscores highscores;
	
	public static int firstLevelIndex = 3;

	// Use this for initialization
	void Start ()
	{
		highscores = GetComponent<Highscores>();
		fx = GameObject.FindObjectOfType<FullScreenFX>();
		fx.SendMessage("OnTitleBeginFX", 1f);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void OnGUI ()
	{
		DrawSelect(new Vector2(100f, 100f));
		DrawSelect(new Vector2(Screen.width - 500f, 100f));
	}

	void DrawSelect(Vector2 origin)
	{
		int numLevels = levelNames.Length;
		Vector2 size = new Vector2(400f, 500f);
		Vector2 buttonsize = new Vector2(100f, 100f);
		float textHeight = 24f;
		//GUI.Box(new Rect(origin.x - 50f, origin.y - 50f, size.x, size.y), "");//, style);

		int buttonsPerRow = 5;

		for(int i=0; i < numLevels; i++)
		{
			int num = i + 1;
			int col = i % buttonsPerRow;
			int row = Mathf.FloorToInt(i / buttonsPerRow);
			string levelName = levelNames[i];

			//if(GUI.Button(new Rect(origin.x + col * 100f, origin.y + row * 100f, buttonsize.x, buttonsize.y), levelName))
			if(GUI.Button(new Rect(origin.x + col * 100f, origin.y + row * 100f, buttonsize.x, buttonsize.y), levelName))
			{
				InputNumber(num);
			}

			string scores = highscores.GetScoresForLevel(firstLevelIndex + i);
			GUI.Label(new Rect(origin.x + col * 100f, origin.y + 150f, buttonsize.x, buttonsize.y), scores);
		}
	}

	void InputNumber(int num)
	{
		StartCoroutine( doLoadLevel(Application.loadedLevel + num) );
	}

	IEnumerator doLoadLevel(int levelNum)
	{
		fx.SendMessage("OnTitleEndFX", 1f);
		yield return new WaitForSeconds(1f);
		
		Application.LoadLevel(levelNum);
	}
}
