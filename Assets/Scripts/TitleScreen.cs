using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour
{
	public float startDelay = 2.0f;

	private float startTime = 0f;

	void Start()
	{
		startTime = Time.time;
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
		yield return new WaitForSeconds(0f);

		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
