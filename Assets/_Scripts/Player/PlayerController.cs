using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Player {

	public event Action Interact;

    private new Rigidbody2D rigidbody;

    [SerializeField] private float speed = 100;
    [SerializeField] private float dashMultiplier = 10;
    [SerializeField] private float dashDelayTime = .5f;

    // The interval of time (in seconds) that the sound will be played.
    [SerializeField] private float moveSoundInterval = .3f;

    private float trackedTime;

    private PlayerHealth health;

    protected override void Awake() {
        base.Awake();

        rigidbody = GetComponent<Rigidbody2D>();

        health = GetComponentInChildren<PlayerHealth>();
    }

	private void OnEnable() {
        userInput.Gameplay.Interact.performed += OnInteract;
        userInput.GameplayWithoutDash.Interact.performed += OnInteract;
		userInput.SoftGameplay.Interact.performed += OnInteract;
		
        userInput.Gameplay.Dash.performed += Dash;

        health.OnHit += OnHit;
    }

    private void OnDisable() {
        userInput.Gameplay.Interact.performed -= OnInteract;
        userInput.GameplayWithoutDash.Interact.performed -= OnInteract;
		userInput.SoftGameplay.Interact.performed -= OnInteract;

        userInput.Gameplay.Dash.performed -= Dash;

        health.OnHit -= OnHit;
    }

    private void FixedUpdate() {
        if (InputManager.Instance.CurrentInputState == InputState.None) return;

        rigidbody.AddForce(move * speed);

        // Increment the timer
        trackedTime += Time.deltaTime;
        
        // Check to see that the proper amount of time has passed
        if ((trackedTime >= moveSoundInterval) && (move != Vector2.zero)) {
            // Play the sound, reset the timer
            AudioManager.Instance.PlayOneShot("PlayerWalk");
            trackedTime = 0;
        }

    }

	private void OnInteract(InputAction.CallbackContext context) => Interact?.Invoke();

    private void OnHit(int currentLives, Vector2 direction) {
        if (currentLives > 0) rigidbody.AddForce(direction);
    }

    private void Dash(InputAction.CallbackContext context) {
        if (!userInput.Gameplay.Movement.IsPressed()) return;

        rigidbody.AddForce(dashMultiplier * speed * move);

        // play sounds here

        StartCoroutine(DashDelay());
    } 

    private IEnumerator DashDelay() {
        userInput.Gameplay.Dash.Disable();

        yield return new WaitForSeconds(dashDelayTime);

        userInput.Gameplay.Dash.Enable();
    }

}
