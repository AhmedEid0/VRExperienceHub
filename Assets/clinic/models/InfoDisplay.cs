using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class InfoDisplay : MonoBehaviour
{
	public Canvas infoCanvas;
	public TextMeshProUGUI infoText;
	public string displayText;
	public static InfoDisplay activeInfo;
	public LayerMask interactableLayer;
	public float requiredInteractionDistance = 5f;
	public Image reticleImage;

	private void Start()
	{
		infoCanvas.gameObject.SetActive(false);
	}

	private void Update()
	{
		RaycastHit hit;
		bool hitDetected = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, requiredInteractionDistance, interactableLayer);

		reticleImage.color = (hitDetected && hit.distance <= requiredInteractionDistance) ? Color.red : Color.white;

		if (Input.GetMouseButtonDown(0))
		{
			if (hitDetected && hit.distance <= requiredInteractionDistance)
			{
				InfoDisplay info = hit.collider.GetComponent<InfoDisplay>();
				if (info != null)
				{
					if (info != activeInfo)
					{
						SwitchActiveInfo(info);
					}
				}
				else if (activeInfo != null)
				{
					activeInfo.HideCanvas();
					activeInfo = null;
				}
			}
			else if (activeInfo != null)
			{
				activeInfo.HideCanvas();
				activeInfo = null;
			}
		}
	}

	private void SwitchActiveInfo(InfoDisplay newInfo)
	{
		if (activeInfo != null)
		{
			activeInfo.HideCanvas();
		}
		activeInfo = newInfo;
		activeInfo.ShowInfo(activeInfo.displayText);
		StartCoroutine(activeInfo.HidePanelAfterDelay(5));
	}

	public void ShowInfo(string message)
	{
		infoText.text = message;
		infoCanvas.gameObject.SetActive(true);
	}

	private IEnumerator HidePanelAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		if (activeInfo == this)
		{
			HideCanvas();
			activeInfo = null;
		}
	}

	public void HideCanvas()
	{
		infoCanvas.gameObject.SetActive(false);
	}
}
