using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : Block {

	public float totalPower {
		private set;
		get;
	}
	public float maxPower {
		private set;
		get;
	}
	public List<float> batts = new List<float> ();

	public float totalFuel {
		private set;
		get;
	}
	public float maxFuel {
		private set;
		get;
	}
	public List<float> tanks = new List<float> ();

	// Use this for initialization
	new void Start () {
		base.Start ();
		Block.core = this;
	}
	
	// Update is called once per frame
	new void Update () {
		
	}
}
