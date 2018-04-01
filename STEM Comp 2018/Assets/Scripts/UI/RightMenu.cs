using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightMenu : MonoBehaviour 
	, IPointerExitHandler
	{


	Block block;
	public Canvas canvas;

	public MouseManager mouseManager;

	public GameObject vanish;

	State[] states;
	StateBox[] stateBoxes;
	KeyStateBox[] keyStateBoxes;

	public GameObject stateBoxPrefab;
	public GameObject keyStateBoxPrefab;

	UIManager uiManager;

	// Use this for initialization
	void Start () {
		InputManager.stateKeyAllowed = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void LoadStates (Block _block, Canvas _canvas, MouseManager _mouseManager, UIManager _uiManager) {
		block = _block;
		canvas = _canvas;
		mouseManager = _mouseManager;
		uiManager = _uiManager;

		states = block.states.ToArray();
		stateBoxes = new StateBox[states.Length];
		keyStateBoxes = new KeyStateBox[states.Length];

		RectTransform scrollBox = gameObject.GetComponent<ScrollRect> ().content;
		scrollBox.sizeDelta = new Vector2 (150, (states.Length * 55) + 5);


		for (int i = 0; i < states.Length; i++) {
			GameObject currentStateBox;
			if (!KeyState.isKeyState (states [i])) {
				currentStateBox = Instantiate (stateBoxPrefab);
			} else {
				currentStateBox = Instantiate (keyStateBoxPrefab);
			}
			currentStateBox.transform.SetParent (scrollBox.gameObject.transform, false);


			if (!KeyState.isKeyState (states [i])) {
				stateBoxes [i] = currentStateBox.GetComponent<StateBox> ();
				stateBoxes [i].state = states [i];
				stateBoxes [i].verticalPos = (-i * 50) - 5;
			} else {
				keyStateBoxes [i] = currentStateBox.GetComponent<KeyStateBox> ();
				keyStateBoxes [i].state = states [i];
				keyStateBoxes [i].verticalPos = (-i * 50) - 5;
			}
		}

	}

	public void OnPointerExit(PointerEventData eventData) {
		CloseMenu ();
	}

	void CloseMenu () {
		mouseManager.allowCameraOrbit = true;
		uiManager.rightMenuOpen = false;
		InputManager.stateKeyAllowed = true;
		Destroy (gameObject);
	}
}
