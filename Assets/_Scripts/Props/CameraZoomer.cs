using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZoomer : MonoBehaviour {

    [SerializeField] private LayerMask playerMask;

    private void OnTriggerEnter2D(Collider2D other) {
        if((1 << other.gameObject.layer | playerMask) != playerMask) return;

        CameraController.Instance.ChangeCamera(CameraState.ForestZoomedCamera);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if((1 << other.gameObject.layer | playerMask) != playerMask) return;

        CameraController.Instance.ChangeCamera(CameraState.ForestCamera);
    }

}
