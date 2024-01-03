using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossHealth : Enemy {

    public bool isInvulnerable = false;

    public new Rigidbody2D rigidbody;

    public Animator animator;

    [SerializeField, Min(1)] private int maxLives = 50;
    [SerializeField] private int currentLives;

    private void Awake() {
        AssignAgent(GetComponentInParent<NavMeshAgent>);

        currentLives = maxLives;
    }

    public void TakeDamage(Vector2 direction, int damage, float knockback) {

        if(isInvulnerable) return;

        currentLives -= damage;

        if (currentLives > 0) rigidbody.AddForce(direction * knockback);

        if(currentLives <= 0) {

            agent.isStopped = true;
            GetComponent<Animator>().SetBool("IsDead", true);

        } else if(((float) currentLives)/maxLives <= 0.5f) {

            GetComponent<Animator>().SetBool("BuffedState", true);

        }
    }

}
