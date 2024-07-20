﻿using UnityEngine;

public class MouseLook : MonoBehaviour
{
	public float mouseSensitivity = 100f;
	float xRotation = 0f;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.localRotation = Quaternion.Euler(xRotation, transform.localEulerAngles.y, 0f);
		transform.parent.Rotate(Vector3.up * mouseX);
	}
}