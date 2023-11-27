using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Player {

	public event Action Interact;

    private new Rigidbody2D rigidbody;

    [SerializeField, Min(0)] private float speed;

    protected override void Awake() {
        base.Awake();

        rigidbody = GetComponent<Rigidbody2D>();
    }

	private void OnEnable() {
		userInput.Gameplay.Interact.performed += OnInteract;
    }

    private void OnDisable() {
		userInput.Gameplay.Interact.performed -= OnInteract;
    }

    private void FixedUpdate() {
        rigidbody.velocity = move * speed;
    }

	private void OnInteract(InputAction.CallbackContext context) => Interact?.Invoke();

}
