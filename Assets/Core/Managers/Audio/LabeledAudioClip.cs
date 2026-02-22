using UnityEngine;

// Labeled AudioClip so it can be accessed with PlaySFX type commands
[System.Serializable]
public class LabeledAudioClip
{
    public string name;
    public AudioClip audioClip;
}