using System;
using System.Collections;
using UnityEngine;
using Ink.Runtime;

public class DialogueManager : Singleton<DialogueManager> {

    private DialogueMenu dialogueMenu;

    private Story story;

    public void ProcessDialogue(TextAsset dialogueText) {
        story = new Story(dialogueText.text);

        StartCoroutine(ProcessDialogueCoroutine());
    }

    private IEnumerator ProcessDialogueCoroutine() {
        InputManager.Instance.ChangeActionMap(InputState.Dialogue);
        MenuManager.Instance.ChangeMenu(MenuState.Dialogue);
        dialogueMenu = MenuManager.Instance.FindMenu(MenuState.Dialogue).GetComponent<DialogueMenu>();

        while (story.canContinue) yield return StartCoroutine(dialogueMenu.SetDialogue(story));

        InputManager.Instance.ChangeActionMap(InputState.Gameplay);
        MenuManager.Instance.ChangeMenu(MenuState.Gameplay);
    }

}
