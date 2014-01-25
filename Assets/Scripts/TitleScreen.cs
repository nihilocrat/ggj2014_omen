using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetButtonDown("Action"))
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
