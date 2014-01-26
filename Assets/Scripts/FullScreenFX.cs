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
	
	void OnPlayerRespawnFX(float time)
	{
		StartCoroutine( doFadeOut(death, time) );
	}

	void OnGameBeginFX(float time)
	{
		StartCoroutine( doFadeOut(transition, time) );
	}
	
	void OnLevelBeginFX(float time)
	{
		StartCoroutine( doFadeOut(win, time) );
	}

	void OnLevelEndFX(float time)
	{
		StartCoroutine( doFadeIn(win, time) );
	}

	void OnTitleBeginFX(float time)
	{
		StartCoroutine( doFadeOut(transition, time) );
	}

	void OnTitleEndFX(float time)
	{
		StartCoroutine( doFadeIn(transition, time) );
	}

	IEnumerator doFadeIn(Transform fx, float time)
	{
		//iTween.Stop(fx.gameObject);

		Color originalColor = fx.renderer.sharedMaterial.color;
		Color startColor = originalColor;
		startColor.a = 0f;

		fx.gameObject.SetActive(true);
		fx.renderer.material.color = startColor;
		iTween.FadeTo(fx.gameObject, 1f, time);

		yield return new WaitForSeconds(time);
		
		//fx.renderer.material.color = originalColor;
		//fx.gameObject.SetActive(false);
	}

	IEnumerator doFadeOut(Transform fx, float time)
	{
		//iTween.Stop(fx.gameObject);

		Color originalColor = fx.renderer.sharedMaterial.color;
		Color startColor = originalColor;
		startColor.a = 1f;

		fx.gameObject.SetActive(true);
		fx.renderer.material.color = startColor;
		iTween.FadeTo(fx.gameObject, 0f, time);

		yield return new WaitForSeconds(time);

		fx.renderer.material.color = originalColor;
		fx.gameObject.SetActive(false);
	}
}
