using System;
using UnityEngine;

public enum DialogueDisplayMode { Menu, World }

public class DialogueTrigger : MonoBehaviour {

    [SerializeField] private TextAsset dialogueText;

	[SerializeField] private DialogueDisplayMode dialogueDisplayMode;

	[SerializeField] private LayerMask playerMask;

	private void OnTriggerEnter2D(Collider2D other) {
		if ((int)Mathf.Pow(2, other.gameObject.layer) == playerMask)
		HandlePlayerInteract(other.gameObject, true);
	}

    private void OnTriggerExit2D(Collider2D other) {
		if ((int)Mathf.Pow(2, other.gameObject.layer) == playerMask)
        HandlePlayerInteract(other.gameObject, false);
    }

    private void HandlePlayerInteract(GameObject player, bool listen) {
        try {
            if (listen) player.GetComponent<PlayerController>().Interact += OnInteract;
            else player.GetComponent<PlayerController>().Interact -= OnInteract;
        } catch (Exception) {}
    }

	private void OnInteract() {
		switch (dialogueDisplayMode) {
			case DialogueDisplayMode.Menu:
				DialogueManager.Instance.ProcessDialogue(dialogueText);
			break; 

			case DialogueDisplayMode.World:
				//TODO: display dialogue in world
			break; 
		}
	}

}
