using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneOnPlayerDied : CutsceneOnEvent {

    [SerializeField] private TimelineAsset CutsceneWithGoblin;
    [SerializeField] private TimelineAsset CutsceneWithoutGoblin;

    [SerializeField] private PlayerHealth playerHealth;

    protected override void Awake() {
        base.Awake();

        if (StoryManager.Instance.GetChoice(ChoiceState.GoblinChoice) == 1)
        director.playableAsset = CutsceneWithGoblin;
        else if (StoryManager.Instance.GetChoice(ChoiceState.GoblinChoice) == 2)
        director.playableAsset = CutsceneWithoutGoblin;
    }

    protected override void OnEnable() {
        base.OnEnable();

        playerHealth.OnDied += PlayCutscene;
    }

    protected override void OnDisable() {
        base.OnDisable();

        playerHealth.OnDied -= PlayCutscene;
    }

    protected override void PlayCutscene() {
        base.PlayCutscene();

        StoryManager.Instance.Proceed();
    }

    protected override void OnPlayableDirectorStopped(PlayableDirector director) {
        base.OnPlayableDirectorStopped(director);

        if (StoryManager.Instance.GetChoice(ChoiceState.GoblinChoice) == 1)
        SceneLoader.Instance.ChangeScene(SceneState.WinEmergencyQuestWithGoblin, true);
        else if (StoryManager.Instance.GetChoice(ChoiceState.GoblinChoice) == 2)
        SceneLoader.Instance.ChangeScene(SceneState.LoseAllTogether, true);
    }
}
