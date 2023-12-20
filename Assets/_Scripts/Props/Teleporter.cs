using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    [SerializeField] private LayerMask playerMask;

    [SerializeField] private CameraState switchCameraTo;

    private void OnTriggerEnter2D(Collider2D other) {
        if ((1 << other.gameObject.layer | playerMask) != playerMask) return;

        SceneLoader.Instance.Crossfade(TeleportPlayer);
    }

    private void TeleportPlayer() {
        GameObject player = GameObject.Find("Player");
        player.transform.position = transform.GetChild(0).position;

        if (switchCameraTo != CameraState.None) CameraController.Instance.ChangeCamera(switchCameraTo);
    }

}
