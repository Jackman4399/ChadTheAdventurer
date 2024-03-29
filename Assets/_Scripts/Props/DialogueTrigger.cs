using System;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public enum DialogueDisplayMode { Menu, World }

public class DialogueTrigger : MonoBehaviour {

    [SerializeField] private TextAsset dialogueText;

    private bool haveVisited;

	[SerializeField] protected LayerMask playerMask;

    private void OnEnable() {
        StoryManager.Instance.OnStoryChanged += OnStoryChanged;
    }

    private void OnDisable() {
        StoryManager.Instance.OnStoryChanged -= OnStoryChanged;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if ((1 << other.gameObject.layer | playerMask) == playerMask) OnInteract();
	}

	protected virtual void OnInteract() {
        DialogueManager.Instance.ProcessDialogue(dialogueText, haveVisited);
        if (!haveVisited) haveVisited = true;
	}

    private void OnStoryChanged(StoryState state) => haveVisited = false;

}
