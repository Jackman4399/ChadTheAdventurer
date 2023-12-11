using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RestrictPlayer : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;

    private void Start() {

        director.stopped += OnPlayableDirectorStopped;
        director.played += OnPlayableDirectorPlayed;

        if (director.playOnAwake) {

            if(MenuManager.Instance != null) MenuManager.Instance.ChangeMenu(MenuState.Dialogue);
        
            if(InputManager.Instance != null) InputManager.Instance.ChangeInput(InputState.None);

            OnPlayableDirectorPlayed(director);
        }
        
    }

    //Will trigger when director finishes
    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector){

            if(MenuManager.Instance != null) MenuManager.Instance.ChangeMenu(MenuState.Gameplay);
            
            if(InputManager.Instance != null) InputManager.Instance.ChangeInput(InputState.Gameplay);
        }
            
    }


    //Will trigger when director starts
    void OnPlayableDirectorPlayed(PlayableDirector aDirector)
    {

        if (director == aDirector){

            if(MenuManager.Instance != null) MenuManager.Instance.ChangeMenu(MenuState.Dialogue);
        
            if(InputManager.Instance != null) InputManager.Instance.ChangeInput(InputState.None);
        }
    }

}
