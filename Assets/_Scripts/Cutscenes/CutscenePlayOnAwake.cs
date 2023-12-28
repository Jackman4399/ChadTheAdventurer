using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutscenePlayOnAwake : MonoBehaviour {
    private PlayableDirector director;

    [SerializeField] private InputState endInputState;
    [SerializeField] private MenuState endMenuState;

    private void Awake() {
        director = GetComponent<PlayableDirector>();
    }

    private void OnEnable() {
        director.stopped += OnPlayableDirectorStopped;
        director.played += OnPlayableDirectorPlayed;
    }

    private void OnDisable() {
        director.stopped -= OnPlayableDirectorStopped;
        director.played -= OnPlayableDirectorPlayed;
    }

    private void Start() {
        if (director.playOnAwake) OnPlayableDirectorPlayed(director);
    }

    //Will trigger when director finishes
    private void OnPlayableDirectorStopped(PlayableDirector director) {
        MenuManager.Instance.ChangeMenu(endMenuState);
        InputManager.Instance.ChangeInput(endInputState);
    }


    //Will trigger when director starts
    private void OnPlayableDirectorPlayed(PlayableDirector director) {
        MenuManager.Instance.ChangeMenu(MenuState.None);
        InputManager.Instance.ChangeInput(InputState.None);
    }

}
