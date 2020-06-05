using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class exampleFPSController : MonoBehaviour {

	GameObject cameraFPS;
	Quaternion initialRotation;
	Vector3 moveDirection = Vector3.zero;
	CharacterController controller;
	float rotationX = 0.0f;
	float rotationY = 0.0f;

	void Start () {
		transform.tag = "Player";
		cameraFPS = GetComponentInChildren (typeof(Camera)).transform.gameObject;
		initialRotation = cameraFPS.transform.localRotation;
		controller = GetComponent<CharacterController>();
	}

	void Update () {
		Vector3 forwardDirection = new Vector3 (cameraFPS.transform.forward.x,0,cameraFPS.transform.forward.z);
		Vector3 sideDirection = new Vector3 (cameraFPS.transform.right.x,0,cameraFPS.transform.right.z);
		forwardDirection.Normalize ();
		sideDirection.Normalize ();
		forwardDirection = forwardDirection * Input.GetAxis ("Vertical");
		sideDirection = sideDirection * Input.GetAxis ("Horizontal");
		Vector3 finalDirection = forwardDirection + sideDirection;
		if (finalDirection.sqrMagnitude > 1) {
			finalDirection.Normalize ();
		}
		if (controller.isGrounded) {
			moveDirection = new Vector3 (finalDirection.x, 0, finalDirection.z);
			moveDirection *= 6.0f;
			if (Input.GetButton ("Jump")) {
				moveDirection.y = 8.0f;
			}
		}
		moveDirection.y -= 20.0f * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		FirstPersonCamera ();
	}

	void FirstPersonCamera(){
		float timeScaleSpeed = 1.0f / Time.timeScale;
		rotationX += Input.GetAxis ("Mouse X") * 10.0f;
		rotationY += Input.GetAxis ("Mouse Y") * 10.0f;
		rotationX = ClampAngleFPS (rotationX, -360, 360);
		rotationY = ClampAngleFPS (rotationY, -80, 80);
		Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
		Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, -Vector3.right);
		Quaternion finalRotation = initialRotation * xQuaternion * yQuaternion;
		cameraFPS.transform.localRotation = Quaternion.Lerp (cameraFPS.transform.localRotation, finalRotation, Time.deltaTime*10.0f*timeScaleSpeed);
	}

	float ClampAngleFPS (float angulo, float min, float max){
		if (angulo < -360F) { angulo += 360F; }
		if (angulo > 360F) { angulo -= 360F; }
		return Mathf.Clamp (angulo, min, max);
	}
}