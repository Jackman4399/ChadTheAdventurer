using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossHealth : Enemy {

    public event Action<int, Vector2> OnHit;

    public bool isInvulnerable = false;

    [SerializeField, Min(1)] private int maxLives = 50;
    [SerializeField] private int currentLives;

    private void Awake() {
        AssignAgent(GetComponentInParent<NavMeshAgent>);

        currentLives = maxLives;
    }

    public void TakeDamage(Vector2 direction, int damage) {

        if(isInvulnerable) return;

        currentLives =- damage;

        OnHit?.Invoke(currentLives, direction); 
        if(currentLives <= 0) {
            agent.isStopped = true;
            //Die animation
            GetComponent<Animator>().SetBool("IsDead", true);

        } else if(currentLives < 25) {

            GetComponent<Animator>().SetBool("BuffedState", true);
            Debug.Log("TEST");

        }
    }

}
