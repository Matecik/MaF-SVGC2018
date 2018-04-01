using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public static bool stateKeyAllowed = true;

	public static KeyCode[] allowedKeys = new KeyCode[] {KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I
		, KeyCode.J, KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X
		, KeyCode.Y, KeyCode.Z};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static bool StateKey (KeyCode keyToTest) {
		if (stateKeyAllowed) {
			return Input.GetKey (keyToTest);
		} 
		return false;
	}

	public static bool StateKeyDown (KeyCode keyToTest) {
		if (stateKeyAllowed) {
			return Input.GetKeyDown (keyToTest);
		} 
		return false;
	}
}
