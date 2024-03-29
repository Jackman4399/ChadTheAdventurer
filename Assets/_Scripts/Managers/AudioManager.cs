using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> {

    [SerializeField] private AudioMixer m_masterMixer;
    public AudioMixer masterMixer => m_masterMixer;

    [SerializeField] private Sound[] sounds;

    protected override void Awake() {
        base.Awake();

        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            AudioSource source = sound.source;

            source.clip = sound.clip;
            source.outputAudioMixerGroup = sound.outputAudioMixerGroup;

            source.volume = sound.volume;
            source.pitch = sound.pitch;

            source.loop = sound.loop;
            source.playOnAwake = sound.playOnAwake;

            if (sound.playOnAwake) source.Play();
        }
    }

    private void Start() {
        OnSceneChanged(SceneLoader.Instance.CurrentSceneState);

        SceneLoader.Instance.OnSceneChanged += OnSceneChanged;
    }

    private void OnDestroy() {
        SceneLoader.Instance.OnSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(SceneState sceneState) {
        switch (sceneState) {
            case SceneState.Main:
                StopAllSounds();
                Play("BGM_MainMenu");
            break;

            case SceneState.TownIntro:
                StopAllSounds();
                Play("BGM1");
            break;

            case SceneState.Cave:
                StopAllSounds();
                Play("BGM_Boss");
            break;

            default: //Should not stop any sounds
            break;
        }
    }

    private Sound GetSound(string name) {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null) { Debug.LogWarning("Sound: " + name + " not found! Returning null."); return null; }
        else return sound;
    }

    /// <summary>
    /// Plays an audio file as a song.
    /// </summary>
    public void Play(string name) {
        Sound sound = GetSound(name);
        sound.source.Play();
    }

    /// <summary>
    /// Plays an audio file as a one-shot.
    /// </summary>
    public void PlayOneShot(string name) {
        Sound sound = GetSound(name);
        sound.source.PlayOneShot(sound.clip);
    }

    public void Stop(string name) {
        Sound sound = GetSound(name);
        sound.source.Stop();
    }

    public void StopAllSounds() {
        foreach (Sound sound in sounds) sound.source.Stop();
    }
    
    public bool IsPlaying(string name) {
        Sound sound = GetSound(name);
        if (sound == null) return false;

        return sound.source.isPlaying;
    }
}
