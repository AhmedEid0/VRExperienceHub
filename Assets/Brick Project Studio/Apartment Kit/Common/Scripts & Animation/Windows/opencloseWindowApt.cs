using System.Collections;
using UnityEngine;

namespace SojaExiles
{
	public class opencloseWindowApt : MonoBehaviour
	{
		public Animator openandclosewindow;
		public bool open;
		public Transform Player;
		public LayerMask interactableLayer; // Set this to your Interactive layer in the inspector

		private bool isPlayerNear;
		private bool isWindowSelected;

		void Start()
		{
			open = false;
			isPlayerNear = false;
			isWindowSelected = false;
		}

		void Update()
		{
			CheckPlayerDistance();

			// Reticle raycast to check if the window is selected
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, ReticleController.CurrentInteractableDistance, interactableLayer))
			{
				isWindowSelected = hit.collider.gameObject == gameObject; // Check if the hit object is this window
			}
			else
			{
				isWindowSelected = false;
			}

			// Click detection for mouse or joystick
			if (isPlayerNear && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton0)))
			{
				if (isWindowSelected)
				{
					if (open)
					{
						StartCoroutine(closing());
					}
					else
					{
						StartCoroutine(opening());
					}
				}
			}
		}

		void CheckPlayerDistance()
		{
			if (Player)
			{
				float dist = Vector3.Distance(Player.position, transform.position);
				isPlayerNear = dist < 5f; // Or whatever your distance threshold is
			}
		}

		IEnumerator opening()
		{
			Debug.Log("You are opening the window.");
			openandclosewindow.Play("Openingwindow");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			Debug.Log("You are closing the window.");
			openandclosewindow.Play("Closingwindow");
			open = false;
			yield return new WaitForSeconds(.5f);
		}
	}
}
