using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	public bool grabable = true;
	[HideInInspector]
	public bool attached = false;
	public float mass = 1f;

	public static List<Block> magicListOfAllBlocks = new List<Block> ();

	public static Core core;

	[HideInInspector]
	public Vector3 desiredRotation;

	protected GameObject robotBase;
	protected Rigidbody rb;

	//public State[] states = new State[0];
	public List<State> states = new List<State>();

	// Use this for initialization
	protected void Start () {
		magicListOfAllBlocks.Add (this);
		rb = gameObject.GetComponent<Rigidbody> ();
	}

	protected void Update () {
		foreach (State state in states) {
			if (KeyState.isKeyState (state)) {
				KeyState.getKeyState (state).Update ();
			}
		}
		if (rb) {
			this.rb.WakeUp ();
		}
	}

	~Block () {
		magicListOfAllBlocks.Remove (this);
	}

	public State getState (string name) {
		foreach (State state in states) {
			if (name == state.stateName) {
				return state;
			}
		}
		return null;
	}

	public List<Block> PerformRayPulse () {
		List<Block> returnBlocks = new List<Block> ();
		for (int i = 0; i < 6; i++) {
			Vector3 testDirection = Vector3.zero;
			RaycastHit hit;
			//Need to use a switch statement here
			if (i == 0) {
				testDirection = gameObject.transform.up;
			}
			if (i == 1) {
				testDirection = -gameObject.transform.up;
			}
			if (i == 2) {
				testDirection = gameObject.transform.right;
			}
			if (i == 3) {
				testDirection = -gameObject.transform.right;
			}
			if (i == 4) {
				testDirection = gameObject.transform.forward;
			}
			if (i == 5) {
				testDirection = -gameObject.transform.forward;
			}
				
			if (Physics.Raycast (gameObject.transform.position, testDirection, out hit, 1.2f,1<<8)) {
				Block hitBlock = hit.collider.gameObject.GetComponent<Block> ();
				if (hitBlock != null) {
					if (hitBlock.attached) {
						returnBlocks.Add (hitBlock);
					}
				}
			}
		}
		return returnBlocks;
	}

	public virtual void SetUpDefaults () {
		grabable = true;
		mass = 1f;
	}



	//-------------------// This area is for doing attaching things
	public void Attach (Transform attachTo, Vector3 normal, GameObject core) {
		if (!attached) {
			Vector3 position = attachTo.position + normal;
			Quaternion rotation = attachTo.rotation * Quaternion.Euler(desiredRotation * 90);
			//Quaternion rotation = Quaternion.Euler(Vector3.Scale(attachTo.eulerAngles, desiredRotation));

			transform.position = position;
			transform.rotation = rotation;
			if (rb) {
				rb.velocity = Vector3.zero;
			}

			Join ();

			attached = true;
		}
	}

	public void Attach (Vector3 position) {
		if (!attached) {
			Quaternion rotation = Quaternion.Euler(Vector3.zero) * Quaternion.Euler(desiredRotation * 90);
			//Quaternion rotation = Quaternion.Euler(Vector3.Scale(attachTo.eulerAngles, desiredRotation));

			transform.parent = Core.core.transform;
			transform.localPosition = position;
			transform.rotation = rotation;
			if (rb) {
				rb.velocity = Vector3.zero;
			}

			Join ();

			attached = true;
		}
	}

	public void Detach () {
		Unjoin ();
		attached = false;
		gameObject.layer = 0;
	}

	void Join () {
		gameObject.transform.parent = core.transform;
		if (GetComponent<Rigidbody> ()) {
			Destroy (GetComponent<Rigidbody> ());
		}
	}

	void Unjoin () {
		if (gameObject != null) {
			gameObject.transform.parent = core.transform.parent;
			gameObject.AddComponent<Rigidbody> ();
		}
	}
}
