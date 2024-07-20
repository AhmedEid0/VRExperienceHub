using UnityEngine;

public class RocketShipController : MonoBehaviour
{
	public float thrust = 1.0f;
	public float rotationSpeed = 100.0f;
	private Rigidbody rocketRigidbody;

	void Start()
	{
		// Get the Rigidbody component for physics-based movement.
		rocketRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		// Basic thrust forward.
		if (Input.GetKey(KeyCode.W))
		{
			rocketRigidbody.AddForce(transform.forward * thrust);
		}

		// Rotation based on input.
		float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
		Quaternion turn = Quaternion.Euler(0f, rotation * Time.fixedDeltaTime, 0f);
		rocketRigidbody.MoveRotation(rocketRigidbody.rotation * turn);
	}
}
