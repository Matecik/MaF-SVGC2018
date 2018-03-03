using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabManager : MonoBehaviour {

	Camera gameCam;
	public GameObject core;

	public Block blockBeingGrabbed;
	bool grabbingBlock = false;

	public float lastDistance = 5;

	// Use this for initialization
	void Start () {
		gameCam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if (grabbingBlock) {
			Hold ();
		}
		if (Input.GetMouseButtonUp (0) && grabbingBlock) {
			Release ();
		}
	}

	public void Grab(Block block) {
		if (block.grabable) {
			if (block.attached) {
				block.Detach ();
			}
			grabbingBlock = true;
			blockBeingGrabbed = block;
			block.gameObject.layer = 2;
			block.gameObject.GetComponent<BoxCollider> ().enabled = false;
		}
	}

	void Release () {
		grabbingBlock = false;
		blockBeingGrabbed.gameObject.layer = 0;
		blockBeingGrabbed.gameObject.GetComponent<BoxCollider> ().enabled = true;
		blockBeingGrabbed = null;
	}

	void Release (int layer) {
		grabbingBlock = false;
		blockBeingGrabbed.gameObject.layer = layer;
		blockBeingGrabbed.gameObject.GetComponent<BoxCollider> ().enabled = true;
		blockBeingGrabbed = null;
	}

	void Hold () {
		Ray ray = gameCam.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;

		Vector3 desiredPos = blockBeingGrabbed.transform.position;

		blockBeingGrabbed.GetComponent<Rigidbody> ().velocity = Vector3.zero;

		if (Physics.Raycast (ray, out hit, MouseManager.maxRange)) {
			Block block = hit.collider.gameObject.GetComponent<Block> ();


			if (block) {
				blockBeingGrabbed.transform.position = block.transform.position + hit.normal;
				blockBeingGrabbed.transform.rotation = block.transform.rotation;
			} else {
				RaycastHit blockHit;
				if (Physics.SphereCast (ray, 1.5f, out blockHit)) {
					Vector3 spherePoint = ray.origin + (ray.direction.normalized * blockHit.distance);
					desiredPos = spherePoint;
					lastDistance = gameCam.WorldToScreenPoint (blockBeingGrabbed.transform.position).z;
				}
			}

			if (block && Input.GetMouseButtonUp (0)) {
				blockBeingGrabbed.Attach (block.transform, hit.normal, core);
				Release (8);
				return;
			}

		} else {
			Vector3 screenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, lastDistance);
			desiredPos = gameCam.ScreenToWorldPoint (screenPoint);
		}

		blockBeingGrabbed.transform.position = Vector3.Lerp (blockBeingGrabbed.transform.position, desiredPos, 0.1f);
	}
}
