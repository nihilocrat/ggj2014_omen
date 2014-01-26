using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class MirrorCamera : MonoBehaviour
{
	public bool doMirror = true;

	void Start()
	{
		// keep this here so we can enable / disable the component
	}

	void OnPreCull ()
	{
		if(doMirror)
		{
			camera.ResetWorldToCameraMatrix ();
			camera.ResetProjectionMatrix ();

			Matrix4x4 mat = camera.projectionMatrix;
			mat *= Matrix4x4.Scale(new Vector3(-1f, 1f, 1f));
			camera.projectionMatrix = mat;
		}
		else
		{
			camera.ResetWorldToCameraMatrix ();
			camera.ResetProjectionMatrix ();
		}
	}
	
	void OnPreRender ()
	{
		if(doMirror)
		{
			GL.SetRevertBackfacing (true);
		}
		else
		{
			GL.SetRevertBackfacing (false);
		}
	}
	
	void OnPostRender ()
	{
		if(doMirror)
		{
			GL.SetRevertBackfacing (false);
		}
		else
		{
			GL.SetRevertBackfacing (false);
		}
	}

}
