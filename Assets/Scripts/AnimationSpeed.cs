using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animation))]
public class AnimationSpeed : MonoBehaviour
{
	public float speed = 1.0f;

	void Start ()
	{
		foreach(AnimationState state in animation)
		{	
			state.speed = speed;	
		}
	}

}
