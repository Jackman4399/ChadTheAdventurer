using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;


public class DialogueMenu : Menu {

    [SerializeField] private TMP_Text nameText, dialogueText;

    [SerializeField] private Button[] choiceButtons;

    [SerializeField] private TMP_Text[] choiceTexts;

    public IEnumerator SetDialogue(Story story) {
        dialogueText.text = story.Continue();
        if (story.currentTags.Count != 0) nameText.text = story.currentTags[0];

        if (story.currentChoices.Count > choiceButtons.Length) {
            Debug.LogWarning("Current choices exceed number of buttons, skipping...");
            yield break;
        } else if (story.currentChoices.Count == 0) {
            foreach (var choiceButton in choiceButtons) choiceButton.gameObject.SetActive(false);
            yield return new WaitForInput(InputManager.Instance.UserInput.Dialogue.Next);
        } else {
            for (int i = 0; i < story.currentChoices.Count; i++) {
                choiceTexts[i].text = story.currentChoices[i].text;
                choiceButtons[i].onClick.RemoveAllListeners();

                // This structure has something to do with how lambda expressions in C# does not copy values from
                // variables outside from its scope, instead they hold a reference to the variable.
                // For lamda expression inside loops, the simple fix is to create a local variable unique to each
                // loop, and get that local variable as a reference for the lambda expression instead.
                int index = i;
                
                choiceButtons[i].onClick.AddListener(() => {
                    story.ChooseChoiceIndex(index);
                    string choiceName = story.currentTags[0];
                    StoryManager.Instance.MakeChoice(Enum.Parse<ChoiceState>(choiceName), index);
                });

                choiceButtons[i].gameObject.SetActive(true);
            }

            yield return new WaitForChoice(story);
        }

    }

}
