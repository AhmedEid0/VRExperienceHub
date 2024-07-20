using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target; // Assign your rocket ship here in the Inspector.
	public Vector3 offset; // Adjust this to set how far back the camera should be from the rocket ship.
	public float smoothSpeed = 0.125f;

	void LateUpdate()
	{
		Vector3 desiredPosition = target.position + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;

		transform.LookAt(target); // Optional: Make the camera always look at the rocket ship.
	}
}
