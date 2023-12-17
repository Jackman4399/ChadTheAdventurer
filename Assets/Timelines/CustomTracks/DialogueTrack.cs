using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackBindingType(typeof(TMP_Text))]
[TrackClipType(typeof(DialogueClip))]
public class DialogueTrack : TrackAsset { 

    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount) {
        return ScriptPlayable<DialogueTrackMixer>.Create(graph, inputCount);
    }

 }
