using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Enemy {

    [SerializeField] private LayerMask playerMask;

    [SerializeField] private float stoppingDistanceFromPlayer;
    public float StoppingDistanceFromPlayer => stoppingDistanceFromPlayer;

    private float originalStoppingDistance = 2;

    private Vector3 origin;

    private new Rigidbody2D rigidbody;

    private EnemyHealth health;

    private void Awake() {
        AssignAgent(GetComponent<NavMeshAgent>);

        rigidbody = GetComponent<Rigidbody2D>();

        origin = transform.position;
        originalStoppingDistance = agent.stoppingDistance;

        health = GetComponentInChildren<EnemyHealth>();
    }

    private void OnEnable() {
        health.OnHit += OnHit;
    }

    private void OnDisable() {
        health.OnHit -= OnHit;
    }

    private void FixedUpdate() {
        rigidbody.AddForce(agent.desiredVelocity);
    }
    
    private void OnTriggerStay2D(Collider2D other) {
        if ((1 << other.gameObject.layer | playerMask) != playerMask) return;
        agent.SetDestination(other.transform.position);
        agent.stoppingDistance = stoppingDistanceFromPlayer;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if ((1 << other.gameObject.layer | playerMask) != playerMask) return;
        agent.SetDestination(origin);
        agent.stoppingDistance = originalStoppingDistance;
    }

    private void OnHit(int currentLives, Vector2 direction) {
        if (currentLives > 0) rigidbody.AddForce(direction);
    }

}
