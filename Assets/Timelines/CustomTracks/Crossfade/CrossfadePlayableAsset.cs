using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class CrossfadePlayableAsset : PlayableAsset {

    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner) {
        var playable = ScriptPlayable<CrossfadePlayableBehaviour>.Create(graph);
        var crossfadeBehaviour = playable.GetBehaviour();
        return playable;
    }

}
