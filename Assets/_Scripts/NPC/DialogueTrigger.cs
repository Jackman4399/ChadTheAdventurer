using System;
using UnityEngine;

public enum DialogueDisplayMode { Menu, World }

public class DialogueTrigger : MonoBehaviour {

    [SerializeField] private TextAsset dialogueText;

	[SerializeField] private LayerMask playerMask;

    private DialogueCue dialogueCue;

    private void Awake() {
        dialogueCue = transform.parent.GetComponentInChildren<DialogueCue>();
    }

	private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("something entered");

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

	private void OnInteract() {
        DialogueManager.Instance.ProcessDialogue(dialogueText);
        dialogueCue.DisableCue();
	}

}
