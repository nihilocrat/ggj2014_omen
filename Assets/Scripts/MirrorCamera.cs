using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class MirrorCamera : MonoBehaviour
{
	void Start ()
	{
		/*
		if(enabled)
		{
			Matrix4x4 mat = camera.projectionMatrix;
			mat *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
			camera.projectionMatrix = mat;
		}
		*/
	}

	void OnPreCull () {
		camera.ResetWorldToCameraMatrix ();
		camera.ResetProjectionMatrix ();
		Matrix4x4 mat = camera.projectionMatrix;
		mat *= Matrix4x4.Scale(new Vector3(-1f, 1f, 1f));
		camera.projectionMatrix = mat;
	}
	
	void OnPreRender () {
		GL.SetRevertBackfacing (true);
	}
	
	void OnPostRender () {
		GL.SetRevertBackfacing (false);
	}

}
