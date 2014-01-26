using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour
{
	public float delay = 1f;
	public float velocityThreshhold = 0.5f;
	public AudioClip footstepSound;

	private CharacterMotor motor;
	private float lastFootstep = 0f;

	public bool IsStepping
	{
		get
		{
			if(motor.movement.velocity.magnitude > velocityThreshhold)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	void Awake()
	{
		motor = GetComponent<CharacterMotor>();
	}

	void FixedUpdate()
	{
		if(IsStepping && Time.fixedTime - lastFootstep > delay)
		{
			var originalPitch = audio.pitch;

			audio.pitch = 0.8f + (Random.value * 0.4f);
			audio.PlayOneShot(footstepSound);
			audio.pitch = originalPitch;
			lastFootstep = Time.fixedTime;
		}
	}
}
