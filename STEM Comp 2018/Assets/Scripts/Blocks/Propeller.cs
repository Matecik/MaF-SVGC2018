using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : Block {

	public float force = 50f;
	public float consumption = 1f;

	void Awake () {
		states.Add(new KeyState ("Active", KeyCode.V, false));
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
			if (Block.core.UsePower (consumption * Time.deltaTime)) {
				core.GetComponent<Rigidbody> ().AddForceAtPosition (gameObject.transform.up * force * Time.deltaTime,gameObject.transform.position);
			}
		}
	}
}
