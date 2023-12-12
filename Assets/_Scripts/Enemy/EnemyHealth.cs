using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyHealth : Enemy {

    public event Action<int, Vector2> OnHit;

    [SerializeField, Min(1)] private int maxLives = 3;
    private int currentLives;

    private void Awake() {
        currentLives = maxLives;
    }

    public void TakeDamage(Vector2 direction) {
        if (currentLives == 0) return;
        currentLives--;
        OnHit?.Invoke(currentLives, direction);
    }

}
