using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Enemy {

    [SerializeField] private LayerMask playerMask;
    public LayerMask PlayerMask => playerMask;

    private Vector3 origin;

    private new Rigidbody2D rigidbody;

    private EnemyHealth health;
    private EnemyAttacker attacker;

    private void Awake() {
        AssignAgent(GetComponent<NavMeshAgent>);

        rigidbody = GetComponent<Rigidbody2D>();

        origin = transform.position;

        health = GetComponentInChildren<EnemyHealth>();
        attacker = GetComponentInChildren<EnemyAttacker>();
    }

    private void OnEnable() {
        health.OnHit += OnHit;
    }

    private void OnDisable() {
        health.OnHit -= OnHit;
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        if ((1 << other.gameObject.layer | playerMask) != playerMask) return;
        agent.SetDestination(other.transform.position);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if ((1 << other.gameObject.layer | playerMask) != playerMask) return;
        agent.SetDestination(origin);
    }

    private void OnHit(int currentLives, Vector2 direction) {
        if (currentLives > 0) rigidbody.AddForce(direction);
    }

}
