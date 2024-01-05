using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
