﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotDataTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TestRobotData () {
		RobotData rd = new RobotData ();
		rd = RobotData.LoadFromFile (@"/Users/ritcma2020/Desktop/test.xml");
		RobotData.CreateRobot(rd, Vector3.up*6, Vector3.zero);
	}

	public void SaveRobot () {
		RobotData rd = new RobotData ();
		RobotData.Save (@"/Users/ritcma2020/Desktop/test.xml", rd);
	}
}
