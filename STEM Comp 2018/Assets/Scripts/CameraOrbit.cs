using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour {

	public Transform objectToOrbit;
	public float distance = 5f;
	public float xSpeed = 100f;
	public float ySpeed = 100f;
	public float scrollSpeed = 20f;

	public MouseManager mouseManager;

	bool rightClick = false;

	float x = 0f;
	float y = 0f;

	// Sets things up
	void Start () {
		// This loads the starting angles of the camera
		Vector3 currentAngle = transform.eulerAngles;
		x = currentAngle.y;
		y = currentAngle.x;

		// This tells the script what object to orbit
		objectToOrbit = transform.parent.gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		//As long as the player holds right click this vaule will be true
		rightClick = Input.GetMouseButton (1) && mouseManager.allowCameraOrbit;

		//This is for scrolling
		distance += Input.GetAxis ("Mouse ScrollWheel") * scrollSpeed * (mouseManager.allowCameraOrbit ? 1 : 0);
		distance = Mathf.Clamp (distance, 2, 20);

		//Does the object we are trying to orbit even exist?
		if (objectToOrbit) {
			//Are we holding right click (This was set at the start of update)
			if (rightClick) {
				//Change the x and y rotations based on any change in mouse position
				x += Input.GetAxis ("Mouse X") * xSpeed * 0.02f;
				y += Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;
			}
			//This prevents the player from flipping the camera over
			y = Mathf.Clamp (y, -89, 89);
			//Loads the new rotation
			Quaternion rotation = Quaternion.Euler (y, x, 0);
			//The position of the camera is based on its rotation. It calculates a position *distance* away from a point at angle *rotation*
			Vector3 distanceBack = new Vector3 (0, 0, -distance);
			Vector3 position = rotation * distanceBack + objectToOrbit.position;
			//Sets the calculated vaules
			transform.rotation = rotation;
			transform.position = position;
		}
	}
}
