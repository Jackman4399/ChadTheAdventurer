using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCave : CutscenePlayOnAwake {

    protected override void Awake() {
        base.Awake();

        if (StoryManager.Instance.GetChoice(ChoiceState.GoblinChoice) == 1) {
            endInputState = InputState.Gameplay;
            // add endMenuState here if menu depends on goblin choice as well
        } else if (StoryManager.Instance.GetChoice(ChoiceState.GoblinChoice) == 2) {
            endInputState = InputState.GameplayWithoutDash;
            // add endMenuState here if menu depends on goblin choice as well
        }
    }

}
