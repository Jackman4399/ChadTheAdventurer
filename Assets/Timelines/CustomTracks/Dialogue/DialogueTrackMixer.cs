using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueTrackMixer : PlayableBehaviour {

    public string dialogueText;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
       var dialogue = playerData as TMP_Text;

        string dialogueText = "";

        if (!dialogue) return;

        int inputCount = playable.GetInputCount();

        for (int i = 0; i < inputCount; i++) {
            float inputWeight = playable.GetInputWeight(i);

            if (inputWeight <= 0) continue;

            var inputPlayable = (ScriptPlayable<DialoguePlayableBehaviour>)playable.GetInput(i);
            DialoguePlayableBehaviour input = inputPlayable.GetBehaviour();

            dialogueText = input.dialogueText;
        }

       dialogue.text = dialogueText;
    }

}
