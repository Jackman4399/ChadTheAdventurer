using UnityEngine;

public enum CameraState { 
    None, 
    TownCamera,
    GuildCamera, 
    HomeCamera,
    PathToForestCamera, 
    ForestCamera, 
    ForestZoomedCamera, 
    EncounterCamera }

public class CameraController : Singleton<CameraController> {

    private Animator animator;

    protected override void Awake() {
        base.Awake();

        animator = GetComponent<Animator>();
    }

    public void ChangeCamera(CameraState cameraState) {
        animator.Play(cameraState.ToString());
    }

}
