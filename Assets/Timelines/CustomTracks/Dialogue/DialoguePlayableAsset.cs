using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class DialoguePlayableAsset : PlayableAsset {

    [SerializeField] private string dialogueText;

    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
        var playable = ScriptPlayable<DialoguePlayableBehaviour>.Create(graph);
        var dialogueBehaviour = playable.GetBehaviour();
        
        dialogueBehaviour.dialogueText = dialogueText;

        return playable;
    }

}
