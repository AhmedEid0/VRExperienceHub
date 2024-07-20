using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BloodvesselSwitcher : MonoBehaviour
{
	public float maxInteractionDistance = 5f; // Maximum distance to detect interactable objects
	public string primarySceneName = "Clinic"; // Primary scene name
	public string secondarySceneName = "Inside Vessels"; // Secondary scene name
	public LayerMask interactableLayer; // Layer for interactable objects

	private FadeToBlack fadeController;

	void Start()
	{
		// Ensure the FadeToBlack component is found once and used throughout
		fadeController = FindObjectOfType<FadeToBlack>();
		if (fadeController == null)
		{
			Debug.LogError("FadeToBlack component not found!");
		}
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			TrySwitchScene();
		}
	}

	private void TrySwitchScene()
	{
		if (SceneManager.GetActiveScene().name == primarySceneName)
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxInteractionDistance, interactableLayer))
			{
				StartCoroutine(SwitchScene(secondarySceneName));
			}
		}
		else
		{
			// Ensure a smooth return by using fade even if the button is clicked anywhere
			StartCoroutine(SwitchScene(primarySceneName));
		}
	}

	private IEnumerator SwitchScene(string sceneName)
	{
		if (fadeController != null)
		{
			yield return StartCoroutine(fadeController.FadeOut());
			SceneManager.LoadScene(sceneName);
			yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);
			yield return StartCoroutine(fadeController.FadeIn());
		}
		else
		{
			// This else block handles cases where FadeToBlack might be missing
			SceneManager.LoadScene(sceneName);
			yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);
			Debug.LogError("FadeToBlack component was not found after attempting to switch scenes.");
		}
	}
}
