﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	public bool grabable = true;
	public bool attached = false;

	public Vector3 desiredRotation;

	GameObject robotBase;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();
		robotBase = transform.parent.gameObject;
	}

	// Update is called once per frame
	void Update () {
		
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
