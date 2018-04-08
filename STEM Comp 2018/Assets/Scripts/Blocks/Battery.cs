using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Block {

	public float power = 30f;
	public float powerCap = 30f;

	Core activeCore;

	// Use this for initialization
	new void Start () {
		base.Start ();
		activeCore = Block.core as Core;
	}

	// Update is called once per frame
	new void Update () {
		base.Update ();
		power = Mathf.Clamp (power, 0, powerCap);
		if (attached && !activeCore.batts.Contains(this)) {
			activeCore.batts.Add(this);
		} else if (!attached && activeCore.batts.Contains(this)) {
			activeCore.batts.Remove (this);
		}
	}
}
