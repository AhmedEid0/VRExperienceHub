using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReticleController : MonoBehaviour
{
	public Image reticleImage; // Assign this in the inspector to your UI Image
	public float interactableDistance = 5f; // Max distance for interaction
	public LayerMask interactableLayer; // Set this to your Interactive layer in the inspector

	private Color originalColor = Color.white; // Keep reticle white as the original color
	private Color interactColor = Color.red; // Color when reticle is over an interactive object, let's use red for visibility
	private bool isInteracting = false; // Track if currently interacting

	public static float CurrentInteractableDistance { get; private set; }

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject); // Make this GameObject persistent
		if (reticleImage == null)
		{
			Debug.LogError("Reticle Image not assigned!");
		}
	}

	void Update()
	{
		if (reticleImage == null) return; // Early exit if reticleImage is not assigned

		RaycastHit hit;
		bool hitInteractable = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactableDistance, interactableLayer);
		CurrentInteractableDistance = hit.distance; // Update the current interactable distance

		if (hitInteractable && hit.distance <= interactableDistance)
		{
			if (!isInteracting)
			{
				StartCoroutine(FadeToColor(interactColor));
				isInteracting = true; // Ensure we set isInteracting here to avoid multiple coroutine calls
			}
		}
		else if (isInteracting)
		{
			StartCoroutine(FadeToColor(originalColor));
			isInteracting = false; // Reset interacting status
		}
	}

	private IEnumerator FadeToColor(Color targetColor)
	{
		float duration = 0.1f; // Duration of the fade
		float currentTime = 0f;

		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			reticleImage.color = Color.Lerp(reticleImage.color, targetColor, currentTime / duration);
			yield return null;
		}
	}

	private void PositionReticle()
	{
		// Ensure the reticle is always positioned correctly
		if (reticleImage != null)
		{
			reticleImage.rectTransform.localPosition = new Vector3(0, 0, 1); // Adjust as needed
		}
	}
}
