using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightMenu : MonoBehaviour 
	, IPointerExitHandler
	{


	Block block;
	public Canvas canvas;

	public MouseManager mouseManager;

	public GameObject vanish;
	bool mouseIsOverPanel = true;

	UIManager uiManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void LoadStates (Block _block, Canvas _canvas, MouseManager _mouseManager, UIManager _uiManager) {
		block = _block;
		canvas = _canvas;
		mouseManager = _mouseManager;
		uiManager = _uiManager;
	}

	public void OnPointerExit(PointerEventData eventData) {
		CloseMenu ();
	}

	void CloseMenu () {
		mouseManager.allowCameraOrbit = true;
		uiManager.rightMenuOpen = false;
		Destroy (gameObject);
	}
}
