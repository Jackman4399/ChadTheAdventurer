using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RestrictPlayer : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;

    [SerializeField] private InputState initInputState;
    [SerializeField] private MenuState initMenuState;

    [SerializeField] private InputState endInputState;
    [SerializeField] private MenuState endMenuState;

    private void Start() {

        director.stopped += OnPlayableDirectorStopped;
        director.played += OnPlayableDirectorPlayed;

        if (director.playOnAwake) {

            if(MenuManager.Instance != null) MenuManager.Instance.ChangeMenu(initMenuState);
        
            if(InputManager.Instance != null) InputManager.Instance.ChangeInput(initInputState);

            OnPlayableDirectorPlayed(director);
        }
        
    }

    //Will trigger when director finishes
    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector){

            if(MenuManager.Instance != null) MenuManager.Instance.ChangeMenu(endMenuState);
            
            if(InputManager.Instance != null) InputManager.Instance.ChangeInput(endInputState);
        }
            
    }


    //Will trigger when director starts
    void OnPlayableDirectorPlayed(PlayableDirector aDirector)
    {

        if (director == aDirector){

            if(MenuManager.Instance != null) MenuManager.Instance.ChangeMenu(initMenuState);
        
            if(InputManager.Instance != null) InputManager.Instance.ChangeInput(initInputState);
        }
    }

}
