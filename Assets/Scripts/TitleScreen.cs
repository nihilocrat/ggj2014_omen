using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour
{
	public float startDelay = 2.0f;

	private float startTime = 0f;
	private FullScreenFX fx;

	void Start()
	{
		startTime = Time.time;
		fx = GameObject.FindObjectOfType<FullScreenFX>();

		fx.SendMessage("OnTitleBeginFX", 2f);
	}

	// Update is called once per frame
	void Update ()
	{
		if(Time.time > startTime + startDelay && Input.GetButtonDown("Action"))
		{
			StartCoroutine( doNextLevel() );
		}
	}

	IEnumerator doNextLevel()
	{
		fx.SendMessage("OnLevelEndFX", 2f);
		yield return new WaitForSeconds(2f);

		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
