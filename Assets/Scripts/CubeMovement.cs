using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class CubeMovement : NetworkBehaviour {

	Rigidbody localRigidBody;
	Transform mainCamera;
	Vector3 cameraOffset;
	[SerializeField]
	float cameraDistance;

	[SerializeField]
	float cameraHeight;

	[SerializeField]
	float movementSpeed;

	[SerializeField]
	float turnSpeed;


	// Use this for initialization
	void Start () {

		localRigidBody = GetComponent<Rigidbody>();
		mainCamera = Camera.main.transform;

		cameraOffset = new Vector3 (0, cameraHeight, -cameraDistance);

		MoveCamera();
		
	}
	
	// Update is called once per frame
	void Update () {

		// Change translation
		float moveAmount = CrossPlatformInputManager.GetAxis("Vertical");
		Vector3 deltaTranslation = transform.position + transform.forward * movementSpeed * moveAmount * Time.deltaTime;
		localRigidBody.MovePosition (deltaTranslation);

		// Change rotation
		float turnAmount = CrossPlatformInputManager.GetAxis("Horizontal");
		Quaternion deltaRotation = Quaternion.Euler (turnSpeed * new Vector3 (0, turnAmount, 0) * Time.deltaTime);
		localRigidBody.MoveRotation (deltaRotation * localRigidBody.rotation);

		MoveCamera();

	}

	void MoveCamera () {
		mainCamera.position = transform.position;
		mainCamera.rotation = transform.rotation;
		mainCamera.Translate (cameraOffset);
	}
}
