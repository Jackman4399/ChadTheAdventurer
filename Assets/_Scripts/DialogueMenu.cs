using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;


public class DialogueMenu : MonoBehaviour {

    [SerializeField] private TMP_Text nameText, dialogueText;

    [SerializeField] private Button[] choiceButtons;

    [SerializeField] private TMP_Text[] choiceTexts;

    public IEnumerator SetDialogue(Story story) {
        dialogueText.text = story.Continue();

        if (story.currentChoices.Count > choiceButtons.Length) {
            Debug.LogWarning("Number of choices in dialogue exceeds choice buttons in dialogue menu." + 
            "Reduce the number of choices or add choice buttons to dialogue menu.");
            yield break;
        } else if (story.currentChoices.Count == 0) {
            Debug.Log("No choice to render.");
            foreach (var choiceButton in choiceButtons) choiceButton.gameObject.SetActive(false);
            yield return new WaitForInput(InputManager.Instance.userInput.Dialogue.Next);
        } else {
            for (int i = 0; i < story.currentChoices.Count; i++) {
                choiceTexts[i].text = story.currentChoices[i].text;
                choiceButtons[i].onClick.RemoveAllListeners();

                // This structure has something to do with how lambda expressions in C# does not copy values from
                // variables outside from its scope, instead they copy a reference to the variable.
                // For lamda expression inside loops, the simple fix is to create a local variable unique to each
                // loop, and get that local variable as a reference for the lambda expression.
                int index = i;
                choiceButtons[i].onClick.AddListener(() => story.ChooseChoiceIndex(index));

                choiceButtons[i].gameObject.SetActive(true);
            }

            yield return new WaitForChoice(story);
        }

    }

}
