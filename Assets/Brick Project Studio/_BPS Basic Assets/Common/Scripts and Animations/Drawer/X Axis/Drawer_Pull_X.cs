using System.Collections;
using UnityEngine;

namespace SojaExiles
{
	public class Drawer_Pull_X : MonoBehaviour
	{
		public Animator pull_01;
		public bool open;
		public Transform Player;
		public LayerMask interactableLayer; // Ensure this is set to your interactable objects layer

		private bool isPlayerNear;
		private bool isDrawerSelected;

		void Start()
		{
			open = false;
			isPlayerNear = false;
			isDrawerSelected = false;
		}

		void Update()
		{
			CheckPlayerDistance();

			// Reticle raycast to check if the drawer is selected
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, ReticleController.CurrentInteractableDistance, interactableLayer))
			{
				isDrawerSelected = hit.collider.gameObject == gameObject; // Check if the hit object is this drawer
			}
			else
			{
				isDrawerSelected = false;
			}

			// Click detection for mouse or joystick
			if (isPlayerNear && isDrawerSelected && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton0)))
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

		void CheckPlayerDistance()
		{
			if (Player)
			{
				float dist = Vector3.Distance(Player.position, transform.position);
				isPlayerNear = dist < 10f; // Distance within which the player can interact with the drawer
			}
		}

		IEnumerator opening()
		{
			Debug.Log("You are opening the drawer.");
			pull_01.Play("openpull_01");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			Debug.Log("You are closing the drawer.");
			pull_01.Play("closepush_01");
			open = false;
			yield return new WaitForSeconds(.5f);
		}
	}
}
