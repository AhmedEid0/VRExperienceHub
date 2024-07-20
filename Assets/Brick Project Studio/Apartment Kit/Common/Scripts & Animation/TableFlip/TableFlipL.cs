using System.Collections;
using UnityEngine;

public class TableFlipL : MonoBehaviour
{
	public Animator FlipL;
	public bool open;
	public Transform Player;
	public LayerMask interactableLayer; // Set this to your Interactive layer in the inspector

	private bool isPlayerNear;
	private bool isTableSelected;

	void Start()
	{
		open = false;
		isPlayerNear = false;
		isTableSelected = false;
	}

	void Update()
	{
		CheckPlayerDistance();

		// Reticle raycast to check if the table is selected
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, ReticleController.CurrentInteractableDistance, interactableLayer))
		{
			isTableSelected = hit.collider.gameObject == gameObject; // Check if the hit object is this table
		}
		else
		{
			isTableSelected = false;
		}

		// Click detection for mouse or joystick
		if (isPlayerNear && isTableSelected && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton0)))
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
			isPlayerNear = dist < 15f; // Adjust the distance as needed
		}
	}

	IEnumerator opening()
	{
		Debug.Log("You are opening the table.");
		FlipL.Play("Lup");
		open = true;
		yield return new WaitForSeconds(.5f);
	}

	IEnumerator closing()
	{
		Debug.Log("You are closing the table.");
		FlipL.Play("Ldown");
		open = false;
		yield return new WaitForSeconds(.5f);
	}
}
