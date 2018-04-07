using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : Block {

	public float fuel = 30f;
	public float fuelCap = 30f;

	Core activeCore;

	// Use this for initialization
	new void Start () {
		base.Start ();
		activeCore = Block.core as Core;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();
		fuel = Mathf.Clamp (fuel, 0, fuelCap);
		if (attached && !activeCore.tanks.Contains(this)) {
			activeCore.tanks.Add(this);
		} else if (!attached && activeCore.tanks.Contains(this)) {
			activeCore.tanks.Remove (this);
		}
	}
}
