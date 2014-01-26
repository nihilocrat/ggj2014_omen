using UnityEngine;
using System.Collections;

public class ScreenSetup : MonoBehaviour
{
	void Awake ()
	{
		Resolution res = Screen.currentResolution;
		Screen.SetResolution(res.width * 2, res.height, true);
	}

}
