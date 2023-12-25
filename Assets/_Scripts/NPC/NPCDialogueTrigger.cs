using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueTrigger : DialogueTrigger {

    private DialogueCue dialogueCue;

    private void Awake() {
        dialogueCue = transform.parent.GetComponentInChildren<DialogueCue>();
    }
    protected override void OnTriggerEnter2D(Collider2D other) {
        if ((1 << other.gameObject.layer | playerMask) == playerMask)
        HandlePlayerInteract(other.gameObject, true);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if ((1 << other.gameObject.layer | playerMask) == playerMask)
        HandlePlayerInteract(other.gameObject, false);
    }

    private void HandlePlayerInteract(GameObject player, bool listen) {
        try {
            if (listen) player.GetComponentInParent<PlayerController>().Interact += OnInteract;
            else player.GetComponentInParent<PlayerController>().Interact -= OnInteract;
        } catch (Exception) { 
            Debug.LogWarning("GameObject has no component named PlayerController, probably " +
            "because it is not a player.");
        }
    }

    protected override void OnInteract() {
        dialogueCue.DisableCue();
        base.OnInteract();
    }

}
