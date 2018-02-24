using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabManager : MonoBehaviour {

	Camera gameCam;
	public GameObject core;

	public Block blockBeingGrabbed;
	bool grabbingBlock;

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
		}
	}

	void Release () {
		grabbingBlock = false;
		blockBeingGrabbed.gameObject.layer = 0;
		blockBeingGrabbed = null;
	}

	void Release (int layer) {
		grabbingBlock = false;
		blockBeingGrabbed.gameObject.layer = layer;
		blockBeingGrabbed = null;
	}

	void Hold () {
		Ray ray = gameCam.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			Block block = hit.collider.gameObject.GetComponent<Block> ();
			if (block && Input.GetMouseButtonUp(0)) {
				blockBeingGrabbed.Attach (block.transform, hit.normal, core);
				Release (8);
			}
		} 
	}
}
