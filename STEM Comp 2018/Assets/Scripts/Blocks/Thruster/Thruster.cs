using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : Block {

	bool thrusterActive = false;

	// Use this for initialization
	new void Start () {
		base.Start ();
		getState ("Active").stateActivated.AddListener (ThrusterActivate);
		getState ("Active").stateDeactivated.AddListener (ThrusterDeactivate);
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
		if (thrusterActive) {
			Debug.Log ("Lift off!");
			gameObject.GetComponent<Rigidbody> ().AddForce (gameObject.transform.up * 50);
		}
	}

	void ThrusterActivate() {
		thrusterActive = true;
	}

	void ThrusterDeactivate() {
		thrusterActive = false;
	}
}
