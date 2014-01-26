using UnityEngine;
using System.Collections;

public class ColorCycler : MonoBehaviour
{
	public Material targetMaterial;
	public Color beginColor;
	public Color endColor;
	public string colorPropertyName = "_Main";
	public float time = 1.0f;

	private Color original;
	private float t = 0f;
	private float sign = 1f;

	void Awake()
	{
		original = targetMaterial.GetColor(colorPropertyName);
	}

	void OnDestroy()
	{
		targetMaterial.SetColor(colorPropertyName, original);
	}

	void Update()
	{
		t += (Time.deltaTime / time) * sign;
		if(t > 1f || t < 0f)
		{
			sign *= -1f;
		}
		/*
		Color newColor = new Color(
			Mathf.Lerp(beginColor.r, endColor.r, t),
			Mathf.Lerp(beginColor.g, endColor.g, t),
			Mathf.Lerp(beginColor.b, endColor.b, t),
			1.0f);
		*/
		targetMaterial.SetColor(colorPropertyName, Color.Lerp(beginColor, endColor, t));
	}
}
