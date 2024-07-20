using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 5f;
	public Transform cameraTransform; // Assign your main camera here in the Inspector
	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		// Calculate forward movement relative to the camera's forward direction
		Vector3 forwardMovement = cameraTransform.forward * moveVertical;
		// Calculate right movement relative to the camera's right direction
		Vector3 rightMovement = cameraTransform.right * moveHorizontal;
		// Combine the movements and normalize
		Vector3 movement = (forwardMovement + rightMovement).normalized;

		// Move the astronaut using Rigidbody
		rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
	}
}
