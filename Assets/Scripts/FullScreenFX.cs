using UnityEngine;
using System.Collections;

public class FullScreenFX : MonoBehaviour
{
	public Transform transition;
	public Transform death;
	public Transform win;

	void OnPlayerDeathFX(float time)
	{
		StartCoroutine( doFadeIn(death, time) );
	}
	
	void OnBeginGameFX(float time)
	{
		StartCoroutine( doFadeOut(transition, time) );
	}

	IEnumerator doFadeIn(Transform fx, float time)
	{
		Color originalColor = fx.renderer.sharedMaterial.color;
		Color startColor = originalColor;
		startColor.a = 0f;

		fx.gameObject.SetActive(true);
		fx.renderer.material.color = startColor;
		iTween.FadeTo(fx.gameObject, 1f, time);

		yield return new WaitForSeconds(time);
		
		fx.renderer.material.color = originalColor;
		fx.gameObject.SetActive(false);
	}

	IEnumerator doFadeOut(Transform fx, float time)
	{
		Color originalColor = fx.renderer.material.color;

		fx.gameObject.SetActive(true);
		iTween.FadeTo(fx.gameObject, 0f, time);

		yield return new WaitForSeconds(time);

		fx.renderer.material.color = originalColor;
		fx.gameObject.SetActive(false);
	}
}
