using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActivationScript : MonoBehaviour
{

    [SerializeField] private PlayableDirector director;

    private void Start() {
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector){

            if(MenuManager.Instance != null) MenuManager.Instance.ChangeMenu(MenuState.Gameplay);
            
            if(InputManager.Instance != null) InputManager.Instance.ChangeInput(InputState.Gameplay);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
            
            if(MenuManager.Instance != null) MenuManager.Instance.ChangeMenu(MenuState.Dialogue);
            
            if(InputManager.Instance != null) InputManager.Instance.ChangeInput(InputState.Menu);

            if(director != null) director.Play();
            
        }

    }

    

}
