using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Player : MonoBehaviour {

    protected UserInput userInput;

    protected Vector2 move;

    protected virtual void Awake() => userInput = InputManager.Instance.userInput;

    protected virtual void OnEnable() {
        userInput.Gameplay.Attack.performed += Attack;
    }

    protected virtual void OnDisable() {
        userInput.Gameplay.Attack.performed -= Attack;
    }


    protected virtual void Update() {
        move = userInput.Gameplay.Movement.ReadValue<Vector2>();
    }

    protected virtual void Attack(InputAction.CallbackContext context) {}

}
