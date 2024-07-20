using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleFade : MonoBehaviour
{
	public float fadeDuration = 1f;
	private Image fadeImage;

	private float buttonPressedTime = 0;
	private float requiredHoldTime = 0.5f;
	private bool isButtonPressed = false;

	void Start()
	{
		fadeImage = GetComponent<Image>();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton0))
		{
			if (!isButtonPressed)
			{
				buttonPressedTime = Time.time;
				isButtonPressed = true;
			}
		}

		if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.JoystickButton0)) && isButtonPressed)
		{
			if (Time.time - buttonPressedTime > requiredHoldTime)
			{
				StartCoroutine(FadeOutIn());
				isButtonPressed = false;
			}
		}
		else
		{
			isButtonPressed = false;
		}
	}

	private IEnumerator FadeOutIn()
	{
		// Fade Out
		for (float t = 0; t < fadeDuration; t += Time.deltaTime)
		{
			fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(t / fadeDuration));
			yield return null;
		}

		// Short wait
		yield return new WaitForSeconds(1f);

		// Fade In
		for (float t = fadeDuration; t > 0; t -= Time.deltaTime)
		{
			fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(t / fadeDuration));
			yield return null;
		}
	}
}
