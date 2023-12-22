using System;
using System.Collections;
using UnityEngine;
using Ink.Runtime;

public class DialogueManager : Singleton<DialogueManager> {

    private DialogueMenu dialogueMenu;

    private Story story;

    private void Start() => 
    dialogueMenu = MenuManager.Instance.FindMenu(MenuState.Dialogue).GetComponent<DialogueMenu>();

    public void ProcessDialogue(TextAsset dialogueText) {
        story = new Story(dialogueText.text);
        StartCoroutine(ProcessDialogueCoroutine());
    }

    private IEnumerator ProcessDialogueCoroutine() {
        InputManager.Instance.ChangeInput(InputState.Dialogue);
        MenuManager.Instance.ChangeMenu(MenuState.Dialogue);

        story.BindExternalFunction("GetCurrentStoryState", () => { 
            return StoryManager.Instance.CurrentStoryState.ToString();
        });

        while (story.canContinue) yield return StartCoroutine(dialogueMenu.SetDialogue(story));

        story.UnbindExternalFunction("GetCurrentStoryState");

        InputManager.Instance.ChangeInput(InputState.Gameplay);
        MenuManager.Instance.ChangeMenu(MenuState.Gameplay);
    }

}
