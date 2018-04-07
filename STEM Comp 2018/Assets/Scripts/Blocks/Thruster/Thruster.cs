using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : Block {

	bool thrusterActive = false;
	public float force = 50f;
	public float consumption = 1f;

	void Awake () {
		states.Add(new KeyState ("Active", KeyCode.B, false));
	}

	// Use this for initialization
	new void Start () {
		base.Start ();
		getState ("Active").stateActivated.AddListener (ThrusterActivate);
		getState ("Active").stateDeactivated.AddListener (ThrusterDeactivate);
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
		if (thrusterActive && attached) {
			if (Block.core.UseFuel (consumption * Time.deltaTime)) {
				gameObject.GetComponent<Rigidbody> ().AddForce (gameObject.transform.up * force);
			}
		}
	}

	void ThrusterActivate() {
		thrusterActive = true;
	}

	void ThrusterDeactivate() {
		thrusterActive = false;
	}
}
