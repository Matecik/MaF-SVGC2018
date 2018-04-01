using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject rightClickMenuPrefab;
	public Canvas canvas;
	public MouseManager mouseManager;

	public bool rightMenuOpen = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenRightClick(Block block) {
		if (rightMenuOpen == false) {
			GameObject menu = Instantiate (rightClickMenuPrefab);
			menu.transform.SetParent (canvas.transform, false);
			RectTransform menuTransform = menu.GetComponent<RectTransform> ();
			Vector2 mosPos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y - Screen.height);

			if (mosPos.y < -Screen.height + 200) {
				mosPos = new Vector2 (mosPos.x, Input.mousePosition.y - Screen.height + 200);
			}
			if (mosPos.x > Screen.width - 150) {
				mosPos = new Vector2 (Input.mousePosition.x - 150, mosPos.y);
			}

			menuTransform.anchoredPosition = mosPos;

			menu.GetComponent<RightMenu> ().LoadStates (block, canvas, mouseManager, this);
			rightMenuOpen = true;
		}
	}
}
