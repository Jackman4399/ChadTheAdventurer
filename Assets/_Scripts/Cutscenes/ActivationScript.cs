using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ActivationScript : MonoBehaviour
{

    [SerializeField] private PlayableDirector director;

    [SerializeField] private InputState endInputState;
    [SerializeField] private MenuState endMenuState;

    private void Awake() {
        director.stopped += OnPlayableDirectorStopped;
    }

    private void OnDestroy() {
        director.stopped -= OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (this.director == director) {

            if(MenuManager.Instance != null) MenuManager.Instance.ChangeMenu(endMenuState);
            
            if(InputManager.Instance != null) InputManager.Instance.ChangeInput(endInputState);
        }
            
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            
            if(MenuManager.Instance != null) MenuManager.Instance.ChangeMenu(MenuState.Dialogue);
            
            if(InputManager.Instance != null) InputManager.Instance.ChangeInput(InputState.Dialogue);

            if(director != null) director.Play();
            
        }

        Destroy(gameObject);

    }

    

}
