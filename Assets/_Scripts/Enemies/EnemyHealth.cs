using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyHealth : Enemy {

    public event Action<int, Vector2> OnHit;

    [SerializeField, Min(1)] private int maxLives = 3;
    private int currentLives;

    private void Awake() {
        AssignAgent(GetComponentInParent<NavMeshAgent>);

        currentLives = maxLives;
    }

    public void TakeDamage(int amount, Vector2 direction) {
        Debug.Log("Enemy, damage took: " + amount);

        currentLives -= amount;
        OnHit?.Invoke(currentLives, direction);
        if (currentLives == 0) agent.isStopped = true;
    }

}
