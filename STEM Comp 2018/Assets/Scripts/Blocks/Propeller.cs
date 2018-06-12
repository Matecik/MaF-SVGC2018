using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : Block {

	public float forceDifference = 50f;
	public float consumption = 1f;
	public float maxForce = 50f;
	public float forcePower = 10f;
	public float upDownPower = 1f;

	public AnimationCurve curveDiff;

	float targetAlt = 10f;
	float requiredUpward = 1f;

	void Awake () {
		states.Add(new KeyState ("Active", KeyCode.Z, true));
		states.Add(new KeyState ("Up", KeyCode.I, false));
		states.Add(new KeyState ("Down", KeyCode.K, false));

	}

	// Use this for initialization
	new void Start () {
		base.Start ();
		getState ("Active").stateActivated.AddListener (PowerOn);
		getState ("Active").stateDeactivated.AddListener (PowerOff);
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
	}

	void FixedUpdate () {

		forceDifference = curveDiff.Evaluate ((targetAlt - transform.position.y)/3) * forcePower;

		if (transform.position.y + core.GetComponent<Rigidbody> ().velocity.y  >= targetAlt) {
			requiredUpward = -Physics.gravity.y - forceDifference;
		} else {
			requiredUpward = -Physics.gravity.y + forceDifference;
		}
			

		if (getState ("Active").isActive && attached) {
			if (Block.core.UsePower (consumption * Time.deltaTime)) {
				core.GetComponent<Rigidbody> ().AddForceAtPosition (gameObject.transform.up * Mathf.Clamp(requiredUpward * core.GetComponent<Rigidbody>().mass,0,maxForce),gameObject.transform.position);
			}
		}

		if (getState ("Active").isActive && attached && getState ("Up").isActive) {
			targetAlt += upDownPower * Time.deltaTime;
		}

		if (getState ("Active").isActive && attached && getState ("Down").isActive) {
			targetAlt -= upDownPower * Time.deltaTime;
		}
	}

	void PowerOn () {
		targetAlt = transform.position.y;
	}

	void PowerOff () {

	}
}
