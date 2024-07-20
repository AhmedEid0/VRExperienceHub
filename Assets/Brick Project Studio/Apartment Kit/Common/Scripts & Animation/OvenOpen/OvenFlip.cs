using System.Collections;
using UnityEngine;

namespace SojaExiles
{
	public class OvenFlip : MonoBehaviour
	{
		public Animator openandcloseoven;
		public bool open;
		public Transform Player;
		public LayerMask interactableLayer; // Set this to your Interactive layer in the inspector

		private bool isPlayerNear;
		private bool isOvenSelected;

		void Start()
		{
			open = false;
			isPlayerNear = false;
			isOvenSelected = false;
		}

		void Update()
		{
			CheckPlayerDistance();

			// Reticle raycast to check if the oven is selected
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, ReticleController.CurrentInteractableDistance, interactableLayer))
			{
				isOvenSelected = hit.collider.gameObject == gameObject; // Check if the hit object is this oven
			}
			else
			{
				isOvenSelected = false;
			}

			// Click detection for mouse or joystick
			if (isPlayerNear && isOvenSelected && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton0)))
			{
				// Toggle the open state each time the oven is clicked
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

		void CheckPlayerDistance()
		{
			if (Player)
			{
				float dist = Vector3.Distance(Player.position, transform.position);
				isPlayerNear = dist < 5f; // Adjust the distance as needed
			}
		}

		IEnumerator opening()
		{
			Debug.Log("You are opening the oven.");
			openandcloseoven.Play("OpenOven");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			Debug.Log("You are closing the oven.");
			openandcloseoven.Play("CloseOven");
			open = false;
			yield return new WaitForSeconds(.5f);
		}
	}
}
