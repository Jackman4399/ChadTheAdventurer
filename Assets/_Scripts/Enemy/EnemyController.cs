using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Enemy {

    [SerializeField] private LayerMask playerMask;

    private Vector3 origin;
    private Vector3 waypoint;

    private bool stopFollow;

    protected override void Awake() {
        base.Awake();

        origin = transform.position;
        waypoint = origin;
    }

    private void Update() { if (stopFollow) agent.SetDestination(origin); else agent.SetDestination(waypoint); }
    
    private void OnTriggerStay2D(Collider2D other) {
        if ((1 << other.gameObject.layer | playerMask) != playerMask) return;
        waypoint = other.transform.position;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if ((1 << other.gameObject.layer | playerMask) != playerMask) return;
        waypoint = origin;
    }

}
