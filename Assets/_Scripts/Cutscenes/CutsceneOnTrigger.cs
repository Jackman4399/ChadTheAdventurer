using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneOnTrigger : CutsceneOnEvent {

    [SerializeField] private LayerMask playerMask;

    [SerializeField] private MenuState additionalMenu;

    [SerializeField] private CameraState switchCameraTo;

    protected override void OnPlayableDirectorStopped(PlayableDirector director) {
        base.OnPlayableDirectorStopped(director);

        if (additionalMenu != MenuState.None) MenuManager.Instance.EnableMenu(additionalMenu);
        if (switchCameraTo != CameraState.None) CameraController.Instance.ChangeCamera(switchCameraTo);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if((1 << other.gameObject.layer | playerMask) != playerMask) return;

        PlayCutscene();
    }

}
