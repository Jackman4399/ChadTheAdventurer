using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class CrossfadePlayableBehaviour : PlayableBehaviour {

    // Called each frame while the state is set to Play
    public override void ProcessFrame(Playable playable, FrameData info, object playerData) {
        if (!SceneLoader.Instance) return;
        
        SceneLoader.Instance.ChangeBackgroundFade(info.weight);
    }

}
