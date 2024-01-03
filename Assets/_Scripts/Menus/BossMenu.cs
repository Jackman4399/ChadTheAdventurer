using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMenu : Menu {

    [SerializeField] private Slider healthBar;

    [SerializeField] private BossHealth bossHealth;

    protected override void Awake() {
        base.Awake();

        healthBar.value = 1;
    }

    private void OnEnable() {
        bossHealth.OnHit += OnHit;
    }

    private void OnDisable() {
        bossHealth.OnHit -= OnHit;
    }

    private void OnHit(int currentLives, int maxLives) {
        healthBar.value = (float)currentLives / maxLives;
    }

}
