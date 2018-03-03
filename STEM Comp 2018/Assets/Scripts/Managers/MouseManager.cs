using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

	Camera gameCam;
	GrabManager grabber;
	public static float maxRange = 40f;

	// Use this for initialization
	void Start () {
		gameCam = Camera.main;
		grabber = gameObject.GetComponent<GrabManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = gameCam.ScreenPointToRay (Input.mousePosition);

		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, maxRange)) {
			Block block = hit.collider.gameObject.GetComponent<Block> ();
			if (block && Input.GetMouseButtonDown (0)) {
				grabber.Grab (block);
			}
		}
	}
}
