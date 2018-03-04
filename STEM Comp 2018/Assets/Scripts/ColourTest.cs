using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourTest : MonoBehaviour {

	public Material offMat;
	public Material onMat;

	public KeyState keyState;

	// Use this for initialization
	void Start () {
		keyState.stateActivated.AddListener (OnMat);
		keyState.stateDeactivated.AddListener (OffMat);
	}

	void Update () {
		keyState.Update ();
	}

	void OnMat() {
		gameObject.GetComponent<MeshRenderer> ().material = onMat;
	}

	void OffMat() {
		gameObject.GetComponent<MeshRenderer> ().material = offMat;
	}
}
