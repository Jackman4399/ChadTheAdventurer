using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum CrossfadeMode { FadeIn, FadeOut }

[System.Serializable]
public class CrossfadePlayableAsset : PlayableAsset {

    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
        var playable = ScriptPlayable<CrossfadePlayableBehaviour>.Create(graph);
        var crossfadeBehaviour = playable.GetBehaviour();
        return playable;
    }

}
