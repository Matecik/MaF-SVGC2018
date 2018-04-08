using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

	Camera gameCam;
	GrabManager grabber;
	public static float maxRange = 40f;

	UIManager uiManager;
	public bool allowRightMenuClick = true;
	public bool allowCameraOrbit = true;

	// Use this for initialization
	void Start () {
		gameCam = Camera.main;
		grabber = gameObject.GetComponent<GrabManager> ();

		uiManager = gameObject.transform.parent.GetComponentInChildren<UIManager> ();
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
			if (block && Input.GetMouseButtonUp (1) && allowRightMenuClick && Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0 && block.states.Count > 0) {
				//Debug.Log ("Open a menu");
				uiManager.OpenRightClick (block);
				allowCameraOrbit = false;
			}
		}
	}
}
