using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RestrictPlayer : MonoBehaviour
{
    private PlayableDirector director;

    [SerializeField] private InputState initInputState;
    [SerializeField] private MenuState initMenuState;

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
        if (director.playOnAwake) {

            if(MenuManager.Instance != null) MenuManager.Instance.ChangeMenu(initMenuState);
        
            if(InputManager.Instance != null) InputManager.Instance.ChangeInput(initInputState);

            OnPlayableDirectorPlayed(director);
        }
        
    }

    //Will trigger when director finishes
    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (this.director == director) {

            if(MenuManager.Instance != null) MenuManager.Instance.ChangeMenu(endMenuState);
            
            if(InputManager.Instance != null) InputManager.Instance.ChangeInput(endInputState);
        }
            
    }


    //Will trigger when director starts
    void OnPlayableDirectorPlayed(PlayableDirector director)
    {

        if (this.director == director) {

            if(MenuManager.Instance != null) MenuManager.Instance.ChangeMenu(initMenuState);
        
            if(InputManager.Instance != null) InputManager.Instance.ChangeInput(initInputState);
        }
    }

}
