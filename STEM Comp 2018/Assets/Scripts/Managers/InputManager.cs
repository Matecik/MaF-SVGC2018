using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	static bool stateKeyAllowed = true;

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
