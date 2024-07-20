using UnityEngine;

public class ExitGameOnClick : MonoBehaviour
{
	public LayerMask interactableLayerMask;
	public void AttemptExitGame()
	{
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, ReticleController.CurrentInteractableDistance, interactableLayerMask))
		{
			if (hit.collider.gameObject == this.gameObject)
			{
				ExitGame();
			}
		}
	}

	private void ExitGame()
	{
		// Add logic here if you want to perform any actions before quitting, like saving the game state.
#if UNITY_EDITOR
		// If running in the Unity Editor
		UnityEditor.EditorApplication.isPlaying = false;
#else
        // If running in a build
        Application.Quit();
#endif
	}
}
