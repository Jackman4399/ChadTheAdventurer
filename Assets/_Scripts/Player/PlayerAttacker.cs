using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : Player {

    public event Action<Direction> OnAttackPressed;

    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private LayerMask bossMask;
    
    [SerializeField, Tooltip("How long should the attack trigger be enabled in physics steps.")] 
    private int attackDuration = 5;

	[SerializeField, Tooltip("How long should the delay between attacks be in seconds.")]
    private float attackDelay = .5f;

    [SerializeField] private int pushbackForce = 1000;

    private PlayerAnimator animator;

    protected override void Awake() {
        base.Awake();

        foreach (Transform child in transform) child.gameObject.SetActive(false);

        animator = transform.parent.GetComponentInChildren<PlayerAnimator>();
    }

    private void OnEnable() {
        InitialiseAttackListeners(true);

        animator.OnAttack += OnAttack;
    }

    private void OnDisable() {
        InitialiseAttackListeners(false);

        animator.OnAttack -= OnAttack;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Vector2 direction = (other.transform.position - transform.position).normalized;

        if ((1 << other.gameObject.layer | enemyMask) == enemyMask) {
            if (StoryManager.Instance.Choices[0].choiceNumber < 2) 
                other.GetComponent<EnemyHealth>().TakeDamage(1, pushbackForce * direction);
            else if (StoryManager.Instance.Choices[0].choiceNumber == 2)
                other.GetComponent<EnemyHealth>().TakeDamage(3, pushbackForce * direction);
        }

        if ((1 << other.gameObject.layer | bossMask) == bossMask){

            if (StoryManager.Instance.Choices[0].choiceNumber < 2) 
                other.GetComponent<BossHealth>().TakeDamage(direction, 1, pushbackForce * 5);
            else if (StoryManager.Instance.Choices[0].choiceNumber == 2)
                other.GetComponent<BossHealth>().TakeDamage(direction, 3, pushbackForce * 10);

        }
        
        
    }

    private void InitialiseAttackListeners(bool enabled) {
		if (enabled) {
			userInput.Gameplay.AttackUp.performed += OnAttackUp;
			userInput.Gameplay.AttackDown.performed += OnAttackDown;
			userInput.Gameplay.AttackLeft.performed += OnAttackLeft;
			userInput.Gameplay.AttackRight.performed += OnAttackRight;

            userInput.GameplayWithoutDash.AttackUp.performed += OnAttackUp;
			userInput.GameplayWithoutDash.AttackDown.performed += OnAttackDown;
			userInput.GameplayWithoutDash.AttackLeft.performed += OnAttackLeft;
			userInput.GameplayWithoutDash.AttackRight.performed += OnAttackRight;
		} else {
			userInput.Gameplay.AttackUp.performed -= OnAttackUp;
			userInput.Gameplay.AttackDown.performed -= OnAttackDown;
			userInput.Gameplay.AttackLeft.performed -= OnAttackLeft;
			userInput.Gameplay.AttackRight.performed -= OnAttackRight;

            userInput.GameplayWithoutDash.AttackUp.performed -= OnAttackUp;
			userInput.GameplayWithoutDash.AttackDown.performed -= OnAttackDown;
			userInput.GameplayWithoutDash.AttackLeft.performed -= OnAttackLeft;
			userInput.GameplayWithoutDash.AttackRight.performed -= OnAttackRight;
		}
	}

    private void OnAttackUp(InputAction.CallbackContext context) => OnAttackDirection(Direction.Up);
	private void OnAttackDown(InputAction.CallbackContext context) => OnAttackDirection(Direction.Down);
	private void OnAttackLeft(InputAction.CallbackContext context) => OnAttackDirection(Direction.Left);
	private void OnAttackRight(InputAction.CallbackContext context) => OnAttackDirection(Direction.Right);

    private void OnAttackDirection(Direction direction) {
        OnAttackPressed?.Invoke(direction);
        StartCoroutine(OnAttackPressedCoroutine());
    }

    private IEnumerator OnAttackPressedCoroutine() {
        InitialiseAttackListeners(false);
        yield return new WaitForSeconds(attackDelay);
        InitialiseAttackListeners(true);
    }

    private void OnAttack(Direction direction) => 
	StartCoroutine(AttackCoroutine(transform.Find("Attack" + direction.ToString()).gameObject));

    private IEnumerator AttackCoroutine(GameObject direction) {

        direction.SetActive(true);

        AudioManager.Instance.PlayOneShot("Slash");

        for (int i = 0; i < attackDuration; i++) yield return new WaitForFixedUpdate();

        direction.SetActive(false);
    }

}
