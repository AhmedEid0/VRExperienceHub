using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageAudioListener : MonoBehaviour
{
	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		var audioListener = GetComponent<AudioListener>();
		if (audioListener != null)
		{
			// Enable the AudioListener only if this camera belongs to the active scene
			audioListener.enabled = gameObject.scene == scene;
		}
	}
}
