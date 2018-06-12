using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;


public class KeyState : State {


	public KeyState (string _stateName, KeyCode _inputKey, bool _isToggleMode) {
		stateName = _stateName;
		inputKey = _inputKey;
		isToggleMode = _isToggleMode;
	}

	public KeyState () {

	}

	[SerializeField]
	public KeyCode inputKey;

	[SerializeField]
	public bool isToggleMode = false;

	public void Update () {
		isActive = UpdateState ();

	}

	public bool setKey (KeyCode targetKey) {
		foreach (KeyCode allowedKey in InputManager.allowedKeys) {
			if (targetKey == allowedKey) {
				inputKey = targetKey;
				return true;
			}
		}
		return false;
	}

	public KeyCode currentKey () {
		return inputKey;
	}
		
	public void ToggleToggleMode () {
		isToggleMode = !isToggleMode;
	}

	public void ToggleToggleMode (bool target) {
		isToggleMode = target;
	}

	public bool getToggle () {
		return isToggleMode;
	}

	bool UpdateState() {
		if (isToggleMode) {
			if (InputManager.StateKeyDown (inputKey)) {
				ChangeInState ();
				return isActive;
			} else {
				return isActive;
			}
		} else {
			if (InputManager.StateKey (inputKey) != isActive) {
				ChangeInState ();
			}
			return InputManager.StateKey (inputKey);
		}
	}

	void ChangeInState() {
		if (!isActive) {
			stateActivated.Invoke ();
		} else {
			stateDeactivated.Invoke ();
		}
	}

	/// <summary>
	/// Returns if the given State was created as a KeyState.
	/// </summary>
	/// <returns><c>true</c>, if the State is a KeyState, <c>false</c> otherwise.</returns>
	/// <param name="state">State to be checked.</param>
	public static bool isKeyState(State state) {
		if (state.GetType().IsSubclassOf(typeof(State))) {
			return true;
		}
		return false;
	}

	/// <summary>
	/// Gets the KeyState from a varible that is only of type State.
	/// </summary>
	/// <remarks>
	/// It might be a good idea to check if a State is a KeyState first
	/// </remarks>
	/// <returns>The KeyState.</returns>
	/// <param name="state">The State to convet to a KeyState.</param>
	/// See <see cref="KeyState.isKeyState(State)"/> for checking if a State if a KeyState
	public static KeyState getKeyState(State state) {
		if (isKeyState (state)) {
			return state as KeyState;
		}
		return null;
	}
}
