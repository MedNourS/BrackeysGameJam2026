using System;
using UnityEngine;

/// <summary>
/// Define all the game's audio clips in-editor here
/// Then use the Instance to play them
/// ex: AudioManager.Instance.PlaySFX("Jump");
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource, sfxSource, dialogueSource;
    [SerializeField] private LabeledAudioClip[] musicClips, sfxClips, dialogueClips; 

    public static AudioManager Instance; // Universally accessible

    // On instantiation, ensure there is only one GameObject that has this manager attached
    // And that it is scene-persistent
    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
            Destroy(gameObject);
    }

    // Music plays the clip forever (until stopped/changed)
    public void PlayMusic(string name) {
        LabeledAudioClip music = Array.Find(musicClips, (s) => s.name == name);

        if (music != null && musicSource != null)
        {
            musicSource.clip = music.audioClip;
            musicSource.Play();
        }
    }
    // SFX and Dialogue play the sound once
    public void PlaySFX(string name)
    {
        LabeledAudioClip sfx = Array.Find(sfxClips, (s) => s.name == name);

        if (sfx != null && sfxSource != null) {
            sfxSource.PlayOneShot(sfx.audioClip);
        }
    }
    public void PlayDialogue(string name) {
        LabeledAudioClip dialogue = Array.Find(dialogueClips, (s) => s.name == name);

        if (dialogue != null && dialogueSource != null)
        {
            sfxSource.PlayOneShot(dialogue.audioClip);
        }
    }
}
