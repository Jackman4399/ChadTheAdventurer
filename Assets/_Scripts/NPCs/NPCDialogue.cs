using System;
using UnityEngine;

public enum DialogueDisplayMode { Menu, World }

public class NPCDialogue : MonoBehaviour {

    [SerializeField] private TextAsset dialogueText;

	[SerializeField] private LayerMask playerMask;

	private void OnTriggerEnter2D(Collider2D other) {
		if ((1 << other.gameObject.layer | playerMask) == playerMask)
		HandlePlayerInteract(other.gameObject, true);
	}

    private void OnTriggerExit2D(Collider2D other) {
		if ((1 << other.gameObject.layer | playerMask) == playerMask)
        HandlePlayerInteract(other.gameObject, false);
    }

    private void HandlePlayerInteract(GameObject player, bool listen) {
        try {
            if (listen) player.GetComponent<PlayerController>().Interact += OnInteract;
            else player.GetComponent<PlayerController>().Interact -= OnInteract;
        } catch (Exception) {}
    }

	private void OnInteract() {
        DialogueManager.Instance.ProcessDialogue(dialogueText);
	}

}
