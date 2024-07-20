using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
	private FadeToBlack fadeController;
	public string gallerySceneName = "GalleryScene"; // Name of your VR gallery scene
	private float buttonPressedTime;
	private bool isButtonPressed = false;
	private bool isSwitchingScene = false;
	public float interactableDistance = 5f;
	public LayerMask interactableLayerMask; // Set this to your interactable layer for images

	// Time required to hold the button to switch back to the gallery scene
	public float requiredHoldTime = 1.5f;

	void Awake()
	{
		fadeController = FindObjectOfType<FadeToBlack>();
	}

	void Update()
	{
		if (isSwitchingScene)
		{
			return;
		}

		if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton0))
		{
			buttonPressedTime = Time.time;
			isButtonPressed = true;
		}

		if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.JoystickButton0)) && isButtonPressed)
		{
			if (IsInteractableLayerHit(out string sceneToLoad))
			{
				// If the user is interacting with an image, switch immediately to that scene without waiting for the hold time.
				StartCoroutine(PerformSceneSwitch(sceneToLoad));
			}
			else if (Time.time - buttonPressedTime > requiredHoldTime)
			{
				// If the user is not interacting with an image but holds the button for the required time, switch back to the gallery scene.
				StartCoroutine(PerformSceneSwitch(gallerySceneName));
			}
		}
		else if (Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.JoystickButton0))
		{
			isButtonPressed = false;
		}
	}

	private bool IsInteractableLayerHit(out string sceneToLoad)
	{
		sceneToLoad = null;
		RaycastHit hit;
		// Use the same interactableDistance as defined in the ReticleController script.
		bool isHit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, ReticleController.CurrentInteractableDistance, interactableLayerMask);
		if (isHit)
		{
			ExitGameOnClick exitGameOnClick = hit.collider.GetComponent<ExitGameOnClick>();
			if (exitGameOnClick != null)
			{
				// If it's the 'Exit Game' object, we check for proximity before allowing exit
				if (Vector3.Distance(Camera.main.transform.position, hit.collider.transform.position) <= interactableDistance)
				{
					exitGameOnClick.AttemptExitGame(); // We attempt to exit the game
					return false; // We return false here because the game exit is being handled by AttemptExitGame, not by a scene switch
				}
			}
			else
			{
				SceneLoaderInfo loaderInfo = hit.collider.GetComponent<SceneLoaderInfo>();
				if (loaderInfo != null)
				{
					sceneToLoad = loaderInfo.sceneName;
					return true;
				}
			}
		}
		return false;
	}


	private IEnumerator PerformSceneSwitch(string sceneToLoad)
	{
		isButtonPressed = false;
		isSwitchingScene = true;

		yield return StartCoroutine(fadeController.FadeOut());

		SceneManager.LoadScene(sceneToLoad);

		yield return StartCoroutine(fadeController.FadeIn());

		isSwitchingScene = false;
	}
}