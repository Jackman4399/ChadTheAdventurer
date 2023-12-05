using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Player {

	public event Action Interact;

    private new Rigidbody2D rigidbody;

    [SerializeField, Min(0)] private float speed;

    // The interval of time (in seconds) that the sound will be played.
    public float interval = 0.3f;

    private float trackedTime = 0.0f;

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

        // Increment the timer
        trackedTime += Time.deltaTime;
        
        // Check to see that the proper amount of time has passed
        if ((trackedTime >= interval) && (rigidbody.velocity != Vector2.zero)) {
            // Play the sound, reset the timer
            AudioManager.Instance.PlayOneShot("PlayerWalk");
            trackedTime = 0.0f;
        }

    }

	private void OnInteract(InputAction.CallbackContext context) => Interact?.Invoke();

}
