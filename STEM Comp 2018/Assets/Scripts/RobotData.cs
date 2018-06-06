using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class RobotData
{
	public List<BlockData> blocks;

	public static bool isLoading = false;

	public static void Save (string filepath, RobotData rd)
	{
		using (FileStream fs = new FileStream (filepath, FileMode.Create)) {
			rd.blocks = BlockData.PrepareBlockData ();
			XmlSerializer xml = new XmlSerializer (typeof(RobotData));
			xml.Serialize (fs, rd);
		}
	}

	public static RobotData LoadFromFile (string filepath) {
		using (FileStream fs = new FileStream (filepath, FileMode.Open)) {
			XmlSerializer xml = new XmlSerializer (typeof(RobotData));
			return (RobotData)xml.Deserialize (fs);
		}
	}

	public static void CreateRobot (RobotData rd, Vector3 position, Vector3 rotation) {
		isLoading = true;
		foreach (Block block in Block.magicListOfAllBlocks) {
			if (block.attached == true && block.GetType() != typeof(Core)) {
				MonoBehaviour.Destroy (block.gameObject);
			}
		}
		Core.core.transform.position = position;
		Core.core.transform.rotation = Quaternion.Euler (rotation);
		Core.core.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		Core.core.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;

		foreach (BlockData blockData in rd.blocks) {
			if (blockData.type != "Core") {
				GameObject blockObject = MonoBehaviour.Instantiate ((GameObject)Resources.Load (blockData.type));
				blockObject.transform.parent = Core.core.transform;
				blockObject.transform.localPosition = 
				new Vector3 (Mathf.Round (blockData.position.x), Mathf.Round (blockData.position.y), Mathf.Round (blockData.position.z));
				blockObject.layer = 8;
				blockObject.GetComponent<BoxCollider> ().enabled = true;
				Block block = blockObject.GetComponent<Block> ();
				block.desiredRotation = blockData.rotation;
				//block.attached = true;
				block.Attach(new Vector3 (Mathf.Round (blockData.position.x), Mathf.Round (blockData.position.y), Mathf.Round (blockData.position.z)));

				foreach (State state in blockData.states) {
					if (KeyState.isKeyState (state)) {
						KeyState ks = block.getState (state.stateName) as KeyState;
						KeyState dks = KeyState.getKeyState (state);
						ks.inputKey = dks.inputKey;
						ks.isActive = dks.isActive;
						if (dks.isActive) {
							ks.stateActivated.Invoke ();
						}
						dks.isToggleMode = dks.isToggleMode;
					} else if (!KeyState.isKeyState (state)) {
						State s = block.getState (state.stateName);
						s.isActive = state.isActive;
						if (state.isActive) {
							s.stateActivated.Invoke ();
						}
					}
				}

			}
		}

		//isLoading = false;
	}
}

public class BlockData
{
	public Vector3 position;
	public Vector3 rotation;

	[XmlAttribute]
	public string type;

	[XmlArray ("State List")]
	[XmlArrayItem ("state")]
	public List<State> states;

	public static List<BlockData> PrepareBlockData () {
		List<BlockData> blockDataList = new List<BlockData> ();
		foreach (Block block in Block.magicListOfAllBlocks) {
			if (block.attached) {
				BlockData bd = new BlockData ();
				bd.position = block.transform.localPosition;
				bd.rotation = block.desiredRotation;
				bd.type = block.GetType ().ToString ();
				bd.states = block.states;
				blockDataList.Add (bd);
			}
		}
		return blockDataList;
	}
}
