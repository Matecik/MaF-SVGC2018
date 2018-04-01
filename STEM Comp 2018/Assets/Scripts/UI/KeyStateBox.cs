using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyStateBox : StateBox {

	KeyState keyState;

	public GameObject keyInput;
	public GameObject toggleMode;

	bool keyIsChanging = false;

	// Use this for initialization
	new void Start () {
		base.Start ();
		keyState = KeyState.getKeyState (base.state);

		keyInput.GetComponentInChildren<Text> ().text = keyState.currentKey().ToString();
		toggleMode.GetComponent<Toggle> ().isOn = keyState.getToggle ();


	}
	
	// Update is called once per frame
	void Update () {
		if (!keyIsChanging) {
			keyInput.GetComponentInChildren<Text> ().text = keyState.currentKey ().ToString ();
		}
		toggleMode.GetComponent<Toggle> ().isOn = keyState.getToggle ();

		if (keyIsChanging) {
			for (int i = 0; i < InputManager.allowedKeys.Length; i++) {
				if (Input.GetKeyDown(InputManager.allowedKeys[i])) {
					if (keyState.setKey (InputManager.allowedKeys [i])) {
						keyIsChanging = false;
					}
				}
			}
		}
	}

	public void changeKey () {
		keyIsChanging = true;
		keyInput.GetComponentInChildren<Text> ().text = "*";
	}

	public void changeToggle () {
		keyState.ToggleToggleMode (toggleMode.GetComponent<Toggle> ().isOn);
	}
}
