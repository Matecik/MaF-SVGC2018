using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateBox : MonoBehaviour {

	public int verticalPos = -5;

	public State state;

	RectTransform rectTrans;
	public GameObject imageLight;
	public GameObject title;

	public Color online;
	public Color offline;

	// Use this for initialization
	protected void Start () {
		rectTrans = gameObject.GetComponent<RectTransform> ();
		rectTrans.anchoredPosition = new Vector2 (0, verticalPos);

		title.GetComponent<Text> ().text = state.stateName;

		state.stateActivated.AddListener (TurnOn);
		state.stateDeactivated.AddListener (TurnOff);

		if (state.isActive) {
			imageLight.GetComponent<Image> ().color = online;
		} else if (!state.isActive) {
			imageLight.GetComponent<Image> ().color = offline;
		}


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TurnOn () {
		imageLight.GetComponent<Image> ().color = online;
	}

	void TurnOff () {
		imageLight.GetComponent<Image> ().color = offline;
	}
}
