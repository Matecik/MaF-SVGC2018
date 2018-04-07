using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	public bool grabable = true;
	[HideInInspector]
	public bool attached = false;

	public static Core core;

	[HideInInspector]
	public Vector3 desiredRotation;

	protected GameObject robotBase;
	protected Rigidbody rb;

	//public State[] states = new State[0];
	public List<State> states = new List<State>();

	// Use this for initialization
	protected void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		robotBase = transform.parent.gameObject;
	}

	protected void Update () {
		foreach (State state in states) {
			if (KeyState.isKeyState (state)) {
				KeyState.getKeyState (state).Update ();
			}
		}
	}

	protected State getState (string name) {
		foreach (State state in states) {
			if (name == state.stateName) {
				return state;
			}
		}
		return null;
	}






	//-------------------// This area is for doing attaching things
	public void Attach (Transform attachTo, Vector3 normal, GameObject core) {
		if (!attached) {
			Vector3 position = attachTo.position + normal;
			Quaternion rotation = attachTo.rotation * Quaternion.Euler(desiredRotation * 90);
			//Quaternion rotation = Quaternion.Euler(Vector3.Scale(attachTo.eulerAngles, desiredRotation));

			transform.position = position;
			transform.rotation = rotation;
			rb.velocity = Vector3.zero;

			Join (core.gameObject.GetComponent<Rigidbody> ());

			attached = true;
		}
	}

	public void Detach () {
		Unjoin ();
		attached = false;
	}

	void Join (Rigidbody bodyToJoin) {
		FixedJoint fixj = gameObject.AddComponent<FixedJoint>();
		fixj.connectedBody = bodyToJoin;
	}

	void Unjoin () {
		FixedJoint[] fixjs = gameObject.GetComponents<FixedJoint> ();
		foreach (FixedJoint fixj in fixjs) {
			Destroy (fixj);
		}
	}
}
