using System;
using System.Collections;
using UnityEngine;
using Ink.Runtime;

public class DialogueManager : Singleton<DialogueManager> {

    private DialogueMenu dialogueMenu;

    public void ProcessDialogue(TextAsset dialogueText, bool haveVisited) =>
    StartCoroutine(ProcessDialogueCoroutine(dialogueText, haveVisited));

    private IEnumerator ProcessDialogueCoroutine(TextAsset dialogueText, bool haveVisited) {
        if (dialogueMenu == null) dialogueMenu = 
        MenuManager.Instance.FindMenu(MenuState.Dialogue).GetComponent<DialogueMenu>();

        var story = new Story(dialogueText.text);

        var inputState = InputManager.Instance.CurrentInputState;
        var menuState = MenuManager.Instance.CurrentMenuState;

        InputManager.Instance.ChangeInput(InputState.Dialogue);
        MenuManager.Instance.ChangeMenu(MenuState.Dialogue);

        story.variablesState["HaveVisited"] = haveVisited;

        story.BindExternalFunction("Proceed", () => StoryManager.Instance.Proceed());

        story.BindExternalFunction("GetCurrentStoryState", () => { 
            return StoryManager.Instance.CurrentStoryState.ToString();
        });

        story.BindExternalFunction("ChangeNextScene", () => SceneLoader.Instance.ChangeNextScene());

        story.BindExternalFunction<string>("ChangeScene", (sceneName) => {
            SceneLoader.Instance.ChangeScene(sceneName);
        });

        story.BindExternalFunction<string>("ChangeInput", (inputName) => {
            InputManager.Instance.ChangeInput(inputName);
        });

        story.BindExternalFunction<string>("ChangeMenu", (menuName) => {
            MenuManager.Instance.ChangeMenu(menuName);
        });

        while (story.canContinue) yield return StartCoroutine(dialogueMenu.SetDialogue(story));

        story.UnbindExternalFunction("Proceed");
        story.UnbindExternalFunction("GetCurrentStoryState");
        story.UnbindExternalFunction("ChangeNextScene");
        story.UnbindExternalFunction("ChangeScene");
        story.UnbindExternalFunction("ChangeInput");
        story.UnbindExternalFunction("ChangeMenu");

        if (InputManager.Instance.CurrentInputState == InputState.Dialogue) 
        InputManager.Instance.ChangeInput(inputState);
        if (MenuManager.Instance.CurrentMenuState == MenuState.Dialogue)
        MenuManager.Instance.ChangeMenu(menuState);
    }

}
