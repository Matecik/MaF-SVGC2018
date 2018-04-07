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
	public List<Battery> batts = new List<Battery> ();

	public float totalFuel {
		private set;
		get;
	}
	public float maxFuel {
		private set;
		get;
	}
	public List<FuelTank> tanks = new List<FuelTank> ();

	void Awake () {
		Block.core = this;
	}

	// Use this for initialization
	new void Start () {
		base.Start ();

	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();

		//Calulate Fuel Cap
		maxFuel = 0;
		foreach (FuelTank tank in tanks) {
			maxFuel += tank.fuelCap;
		}
		//Calculate Fuel
		totalFuel = 0;
		foreach (FuelTank tank in tanks) {
			totalFuel += tank.fuel;
		}
		//Automaticly balence fuel by averaging all fuel tank's fuel
		foreach (FuelTank tank in tanks) {
			tank.fuel = totalFuel / tanks.Count;
		}
		Mathf.Clamp (totalFuel, 0, maxFuel);

		//Calulate Fuel Cap
		maxPower = 0;
		foreach (Battery batt in batts) {
			maxPower += batt.powerCap;
		}
		//Calculate Fuel
		totalPower = 0;
		foreach (Battery batt in batts) {
			totalPower += batt.power;
		}
		//Automaticly balence fuel by averaging all fuel tank's fuel
		foreach (Battery batt in batts) {
			batt.power = totalPower / batts.Count;
		}
		Mathf.Clamp (totalPower, 0, maxPower);
	}

	public bool UseFuel (float usage) {
		if (totalFuel - usage >= 0) {
			totalFuel -= usage;
			foreach (FuelTank tank in tanks) {
				tank.fuel = totalFuel / tanks.Count;
			}
			return true;
		}
		return false;
	}

	public bool UsePower (float usage) {
		if (totalPower - usage >= 0) {
			totalPower -= usage;
			foreach (Battery batt in batts) {
				batt.power = totalPower / batts.Count;
			}
			return true;
		}
		return false;
	}

	public void AddFuel (float ammount) {
		totalFuel += ammount;
		foreach (FuelTank tank in tanks) {
			tank.fuel = totalFuel / tanks.Count;
		}
		Mathf.Clamp (totalFuel, 0, maxFuel);
	}

	public void AddPower (float ammount) {
		totalPower += ammount;
		foreach (Battery batt in batts) {
			batt.power = totalPower / batts.Count;
		}
		Mathf.Clamp (totalPower, 0, maxPower);
	}
}
