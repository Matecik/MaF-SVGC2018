using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : Block {

	public GameObject wheel;
	HingeJoint hinge;
	JointMotor motor;

	public float speedFactor = 0.1f;
	public float wheelSpeed = 1f;
	public float wheelPower = 1f;

	public float consumption = 1f;

	Vector3 desiredPos;
	Vector3 desiredScale;

	void Awake () {
		states.Add (new KeyState ("Extended", KeyCode.P, true));
		states.Add (new KeyState ("Forward", KeyCode.T, false));
		states.Add (new KeyState ("Backward", KeyCode.G, false));
		desiredPos = Vector3.zero;
		desiredScale = new Vector3 (0.9f, 0.3f, 0.9f);
	}

	// Use this for initialization
	new void Start () {
		base.Start ();
		getState ("Extended").stateActivated.AddListener (Extend);
		getState ("Extended").stateDeactivated.AddListener (Retract);
		Physics.IgnoreCollision (gameObject.GetComponent<Collider>(), wheel.GetComponent<Collider>(), true);
	}

	// Update is called once per frame
	new void Update () {
		base.Update ();
		wheel.transform.localPosition = Vector3.Lerp (wheel.transform.localPosition, desiredPos, speedFactor);
		if (!attached) {
			wheel.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
		wheel.transform.localScale = Vector3.Lerp (wheel.transform.localScale, desiredScale, speedFactor);
		wheel.GetComponent<CapsuleCollider> ().enabled = gameObject.GetComponent<BoxCollider> ().enabled;

		if (attached) {
			if (!hinge) {
				hinge = wheel.AddComponent<HingeJoint> ();
				hinge.anchor = wheel.transform.localPosition;
				hinge.useMotor = true;
				hinge.axis = Vector3.up;
				motor = hinge.motor;
				motor.force = wheelPower;
				hinge.motor = motor;
				hinge.connectedBody = core.GetComponent<Rigidbody> ();
			} else if (hinge) {
				hinge.anchor = wheel.transform.localPosition;
			}
		} else {
			hinge = wheel.GetComponent<HingeJoint> ();
			if (hinge) {
				Destroy (hinge);
			}
		}

		if (getState ("Forward").isActive && attached && core.UsePower(consumption * Time.deltaTime)) {
			motor.targetVelocity = wheelSpeed;
			hinge.motor = motor;
		} else if (getState ("Backward").isActive && attached && core.UsePower(consumption * Time.deltaTime)) {
			motor.targetVelocity = -wheelSpeed;
			hinge.motor = motor;
		} else if (attached) {
			motor.targetVelocity = 0;
			hinge.motor = motor;
		}
	}

	void Extend () {
		if (attached) {
			desiredPos = new Vector3 (0, -0.5f, 0);
			desiredScale = new Vector3 (1.1f, 0.3f, 1.1f);
			grabable = false;
		}
	}

	void Retract () {
		if (attached) {
			desiredPos = Vector3.zero;
			desiredScale = new Vector3 (0.9f, 0.3f, 0.9f);
			grabable = true;
		}
	}
}
