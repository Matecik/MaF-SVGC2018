using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyroscope : Block {

	public float tourqe = 50f;
	public float consumption = 1f;


	void Awake () {
		states.Add(new State ("Powered"));
		states.Add(new KeyState ("Active", KeyCode.F, false));
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
		if (getState ("Active").isActive && attached) {
			core.GetComponent<Rigidbody> ().AddTorque (((transform.eulerAngles.x % 360f)-180f)*tourqe,0,((transform.eulerAngles.z % 360f)-180f)*tourqe);
		}
	}


}
