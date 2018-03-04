using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "State")]
public class State : ScriptableObject {

	public string stateName = "State";

	public UnityEvent stateActivated;
	public UnityEvent stateDeactivated;

	[SerializeField]
	protected bool isActive = false;

	void Start() {
		stateActivated = new UnityEvent ();
		stateDeactivated = new UnityEvent ();
	}
		
	public void ForceSetState (bool targetState) {
		if (targetState) {
			isActive = true;
			stateActivated.Invoke ();
		} else if (!targetState) {
			isActive = false;
			stateDeactivated.Invoke ();
		}
	}

}
