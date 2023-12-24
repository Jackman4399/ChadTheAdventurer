using System;
using System.Collections;
using UnityEngine;
using Ink.Runtime;

public class DialogueManager : Singleton<DialogueManager> {

    private DialogueMenu dialogueMenu;

    private void Start() => 
    dialogueMenu = MenuManager.Instance.FindMenu(MenuState.Dialogue).GetComponent<DialogueMenu>();

    public void ProcessDialogue(TextAsset dialogueText, bool haveVisited) =>
    StartCoroutine(ProcessDialogueCoroutine(dialogueText, haveVisited));

    private IEnumerator ProcessDialogueCoroutine(TextAsset dialogueText, bool haveVisited) {
        var story = new Story(dialogueText.text);

        InputManager.Instance.ChangeInput(InputState.Dialogue);
        MenuManager.Instance.ChangeMenu(MenuState.Dialogue);

        story.variablesState["HaveVisited"] = haveVisited;

        story.BindExternalFunction("Proceed", () => StoryManager.Instance.Proceed());
        story.BindExternalFunction("GetCurrentStoryState", () => { 
            return StoryManager.Instance.CurrentStoryState.ToString();
        });

        while (story.canContinue) yield return StartCoroutine(dialogueMenu.SetDialogue(story));

        story.UnbindExternalFunction("Proceed");
        story.UnbindExternalFunction("GetCurrentStoryState");

        InputManager.Instance.ChangeInput(InputState.Gameplay);
        MenuManager.Instance.ChangeMenu(MenuState.Gameplay);
    }

}
