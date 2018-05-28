using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class RobotData
{
	public List<BlockData> blocks;

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
				bd.position = block.transform.position;
				bd.rotation = block.desiredRotation;
				bd.type = block.GetType ().ToString ();
				bd.states = block.states;
				blockDataList.Add (bd);
			}
		}
		return blockDataList;
	}
}
