using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicPart", menuName = "Parts/MusicPart", order = 1)]
public class MusicPart : ScriptableObject {

    public string partName;

    public AudioClip AudioClip;

    [Range(0f,1f)]
    public float volume;
    [Range(0.1f,2f)]
    public float pitch = 1f;

    [HideInInspector]
    public AudioSource source;

}
