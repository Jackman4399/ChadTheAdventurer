using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Player {

	public event Action Interact;

    private new Rigidbody2D rigidbody;

    [SerializeField, Min(0)] private float speed;

    // The interval of time (in seconds) that the sound will be played.
    [SerializeField, Min(0)] private float interval = .3f;

    private float trackedTime;

    private PlayerHealth health;

    protected override void Awake() {
        base.Awake();

        rigidbody = GetComponent<Rigidbody2D>();

        health = GetComponentInChildren<PlayerHealth>();
    }

	private void OnEnable() {
		userInput.SoftGameplay.Interact.performed += OnInteract;
		userInput.Gameplay.Interact.performed += OnInteract;

        health.OnHit += OnHit;
    }

    private void OnDisable() {
		userInput.SoftGameplay.Interact.performed -= OnInteract;
		userInput.Gameplay.Interact.performed -= OnInteract;

        health.OnHit += OnHit;
    }

    private void FixedUpdate() {
        if (InputManager.Instance.CurrentInputState == InputState.None) return;

        rigidbody.velocity = move * speed;

        // Increment the timer
        trackedTime += Time.deltaTime;
        
        // Check to see that the proper amount of time has passed
        if ((trackedTime >= interval) && (rigidbody.velocity != Vector2.zero)) {
            // Play the sound, reset the timer
            AudioManager.Instance.PlayOneShot("PlayerWalk");
            trackedTime = 0;
        }

    }

	private void OnInteract(InputAction.CallbackContext context) => Interact?.Invoke();

    private void OnHit(int currentLives, Vector2 direction) {
        if (currentLives > 0) rigidbody.AddForce(direction);
    }

}
