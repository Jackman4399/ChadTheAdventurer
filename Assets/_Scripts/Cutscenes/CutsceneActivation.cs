using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneActivation : MonoBehaviour {

    private PlayableDirector director;

    [SerializeField] private LayerMask playerMask;

    private InputState currentInputState;
    private MenuState currentMenuState;

    private void Awake() {
        director = GetComponent<PlayableDirector>();
    }

    private void OnEnable() {
        director.stopped += OnPlayableDirectorStopped;
    }

    private void OnDisable() {
        director.stopped -= OnPlayableDirectorStopped;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if((1 << other.gameObject.layer | playerMask) != playerMask) return;

        currentMenuState = MenuManager.Instance.CurrentMenuState;
        currentInputState = InputManager.Instance.CurrentInputState;

        MenuManager.Instance.ChangeMenu(MenuState.None);
        InputManager.Instance.ChangeInput(InputState.None);

        SceneLoader.Instance.Crossfade(director.Play, TransitionType.Cutscene);
    }

    void OnPlayableDirectorStopped(PlayableDirector director) {
        MenuManager.Instance.ChangeMenu(currentMenuState);
        InputManager.Instance.ChangeInput(currentInputState);

        Destroy(gameObject);
    }

}
