using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Xml.Serialization;


[XmlInclude(typeof(KeyState))]
public class State {

	public State (string _stateName) {
		stateName = _stateName;
		Start ();
	}

	protected State () {
		Start ();
	}

	[XmlAttribute]
	public string stateName = "State";

	[XmlIgnore]
	public UnityEvent stateActivated;
	[XmlIgnore]
	public UnityEvent stateDeactivated;

	[SerializeField]
	public bool isActive = false;

	void Start() {
		stateActivated = new UnityEvent ();
		stateDeactivated = new UnityEvent ();
		stateActivated.AddListener (OnActivate);
		stateDeactivated.AddListener (OnDeactiviate);
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

	void OnActivate () {
		isActive = true;
	}

	void OnDeactiviate () {
		isActive = false;
	}

}
