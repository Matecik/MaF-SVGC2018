using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Block {

	public float inFlow = 1f;
	public float outFlow = 1f;

	bool batIsFull = false;

	void Awake () {
		states.Add(new KeyState ("Active", KeyCode.L, true));
	}

	// Use this for initialization
	new void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();

		batIsFull = core.totalPower == core.maxPower;

		if (getState ("Active").isActive && attached && !batIsFull) {
			if (Core.core.UseFuel (inFlow * Time.deltaTime)) {
				Core.core.AddPower (outFlow * Time.deltaTime);
			}
		}
	}
}
