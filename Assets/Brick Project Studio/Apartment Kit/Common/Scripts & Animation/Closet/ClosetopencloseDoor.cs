using System.Collections;
using UnityEngine;

namespace SojaExiles
{
	public class ClosetopencloseDoor : MonoBehaviour
	{
		public Animator Closetopenandclose;
		public bool open;
		public Transform Player;
		public LayerMask interactableLayer; // Set this to your Interactive layer in the inspector

		private bool isPlayerNear;
		private bool isDoorSelected;

		void Start()
		{
			open = false;
			isPlayerNear = false;
			isDoorSelected = false;
		}

		void Update()
		{
			CheckPlayerDistance();

			// Reticle raycast to check if the closet door is selected
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, ReticleController.CurrentInteractableDistance, interactableLayer))
			{
				isDoorSelected = hit.collider.gameObject == gameObject; // Check if the hit object is this closet door
			}
			else
			{
				isDoorSelected = false;
			}

			// Click detection for mouse or joystick
			if (isPlayerNear && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton0)))
			{
				if (isDoorSelected)
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
				isPlayerNear = dist < 5f; // Adjust the distance as needed
			}
		}

		IEnumerator opening()
		{
			Debug.Log("You are opening the closet door.");
			Closetopenandclose.Play("ClosetOpening");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			Debug.Log("You are closing the closet door.");
			Closetopenandclose.Play("ClosetClosing");
			open = false;
			yield return new WaitForSeconds(.5f);
		}
	}
}
