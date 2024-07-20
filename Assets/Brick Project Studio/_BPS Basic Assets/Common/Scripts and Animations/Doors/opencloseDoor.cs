using System.Collections;
using UnityEngine;

namespace SojaExiles
{
	public class opencloseDoor : MonoBehaviour
	{
		public Animator openandclose;
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

			// Reticle hover detection
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, ReticleController.CurrentInteractableDistance, interactableLayer))
			{
				isDoorSelected = hit.collider.gameObject == gameObject;
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
				isPlayerNear = dist < 5f; // Or whatever your distance threshold is
			}
		}

		IEnumerator opening()
		{
			Debug.Log("You are opening the door.");
			openandclose.Play("Opening");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			Debug.Log("You are closing the door.");
			openandclose.Play("Closing");
			open = false;
			yield return new WaitForSeconds(.5f);
		}
	}
}
