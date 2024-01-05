using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneOnBossDied : CutsceneOnEvent {

    [SerializeField] private BossHealth bossHealth;

    protected override void OnEnable() {
        base.OnEnable();

        bossHealth.OnDied += PlayCutscene;
    }

    protected override void OnDisable() {
        base.OnDisable();

        bossHealth.OnDied -= PlayCutscene;
    }

    protected override void PlayCutscene() {
        base.PlayCutscene();

        StoryManager.Instance.Proceed();
    }

    protected override void OnPlayableDirectorStopped(PlayableDirector director) {
        base.OnPlayableDirectorStopped(director);

        SceneLoader.Instance.ChangeScene(SceneState.WinEmergencyQuest, true);
    }
}
