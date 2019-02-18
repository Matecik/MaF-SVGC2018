using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public static MusicPart[] musics;

	// Use this for initialization
	void Awake () {
		foreach (MusicPart p in musics)
        {
            p.source = gameObject.AddComponent<AudioSource>();
            p.source.clip = p.AudioClip;
            p.source.volume = p.volume;
            p.source.pitch = p.pitch;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void Play (string name)
    {
        Array
    }
}
