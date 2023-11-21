using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player {

    private new Rigidbody2D rigidbody;

    [SerializeField, Min(0)] private float speed;

    protected override void Awake() {
        base.Awake();

        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        rigidbody.velocity = move * speed;
    }

}
