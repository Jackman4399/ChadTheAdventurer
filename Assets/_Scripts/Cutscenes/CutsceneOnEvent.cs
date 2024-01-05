using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public abstract class CutsceneOnEvent : MonoBehaviour {

    protected PlayableDirector director;

    protected InputState currentInputState;
    protected MenuState currentMenuState;

    protected virtual void Awake() {
        director = GetComponent<PlayableDirector>();
    }

    protected virtual void OnEnable() {
        director.stopped += OnPlayableDirectorStopped;
    }

    protected virtual void OnDisable() {
        director.stopped -= OnPlayableDirectorStopped;
    }

    protected virtual void PlayCutscene() {
        currentMenuState = MenuManager.Instance.CurrentMenuState;
        currentInputState = InputManager.Instance.CurrentInputState;

        MenuManager.Instance.ChangeMenu(MenuState.None);
        InputManager.Instance.ChangeInput(InputState.None);

        SceneLoader.Instance.Crossfade(director.Play, TransitionType.Cutscene);
    }

    protected virtual void OnPlayableDirectorStopped(PlayableDirector director) {
        MenuManager.Instance.ChangeMenu(currentMenuState);
        InputManager.Instance.ChangeInput(currentInputState);

        Destroy(gameObject);
    }

}
