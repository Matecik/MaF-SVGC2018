using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : Block {


	public float tourqeModifier = 50f;
	public float turnModifier = 35f;
	public float consumption = 1f;
	public float maxTourqe = 200f;

	public AnimationCurve maxiumSpeedCurve; 
	public float stoppingPowerMofifier;

	public float angleChangePower = 1f;

	Vector3 targetDirection = Vector3.up;

	void Awake () {
		states.Add(new State ("Powered"));
		states.Add(new KeyState ("Active", KeyCode.F, true));
		states.Add(new KeyState ("Forward", KeyCode.W, false));
		states.Add(new KeyState ("Backward", KeyCode.S, false));
		states.Add(new KeyState ("Right", KeyCode.D, false));
		states.Add(new KeyState ("Left", KeyCode.A, false));
		states.Add(new KeyState ("Turn Right", KeyCode.E, false));
		states.Add(new KeyState ("Turn Left", KeyCode.Q, false));
	}

	// Use this for initialization
	new void Start () {
		base.Start ();
	}

	// Update is called once per frame
	new void Update () {
		base.Update ();
	}

	void FixedUpdate () {
		int forwardInt = getState ("Forward").isActive ? 1 : 0;
		int backwardInt = getState ("Backward").isActive ? -1 : 0;
		int rightInt = getState ("Right").isActive ? 1 : 0;
		int leftInt = getState ("Left").isActive ? -1 : 0;
		targetDirection = new Vector3 (
			((leftInt+rightInt)*angleChangePower),
			1,
			((backwardInt+forwardInt)*angleChangePower)
		);
		targetDirection = targetDirection.normalized;
		targetDirection = Quaternion.Euler (0, core.transform.rotation.eulerAngles.y, 0) * targetDirection;


		if (getState ("Active").isActive && attached) {
			Vector3 target = targetDirection;

			//get the angle between transform.forward and target delta
			float angleDiff = Vector3.Angle(transform.up, target);

			// get its cross product, which is the axis of rotation to
			// get from one vector to the other
			Vector3 cross = Vector3.Cross(transform.up, target);

			float desiredMaxSpeed = maxiumSpeedCurve.Evaluate (angleDiff / 180);

			// apply torque along that axis according to the magnitude of the angle.
			if (core.UsePower (((angleDiff/180) * tourqeModifier * consumption) * Time.deltaTime)) {
				core.GetComponent<Rigidbody> ().AddTorque (cross * Mathf.Clamp((angleDiff * tourqeModifier),0,maxTourqe));
			}

			if (core.GetComponent<Rigidbody> ().angularVelocity.magnitude > desiredMaxSpeed) {
				core.GetComponent<Rigidbody> ().AddTorque (Vector3.ClampMagnitude(-core.GetComponent<Rigidbody> ().angularVelocity * stoppingPowerMofifier,100f));
			}

//			if (angleDiff < 0.02f) {
//				float yRot = core.transform.localRotation.eulerAngles.y;
//				core.transform.up = target;
//				core.transform.localRotation = Quaternion.Euler (core.transform.rotation.x, yRot, core.transform.position.z);
//				
//			}
		}

		if (getState ("Turn Right").isActive && attached && getState ("Active").isActive) {
			if (core.UsePower (consumption * Time.deltaTime)) {
				core.GetComponent<Rigidbody> ().AddTorque (new Vector3 (0, turnModifier, 0));
			}
		}
		if (getState ("Turn Left").isActive && attached && getState ("Active").isActive) {
			if (core.UsePower (consumption * Time.deltaTime)) {
				core.GetComponent<Rigidbody>().AddTorque( new Vector3 (0,-turnModifier,0));
			}
		}
	}


}
