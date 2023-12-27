using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Player : MonoBehaviour {

    protected UserInput userInput;

    protected Vector2 move;

    protected virtual void Awake() => userInput = InputManager.Instance.UserInput;


    protected virtual void Update() {
        move = InputManager.Instance.CurrentInputState switch {
            InputState.Gameplay => userInput.Gameplay.Movement.ReadValue<Vector2>(),
            InputState.GameplayWithoutDash => userInput.GameplayWithoutDash.Movement.ReadValue<Vector2>(),
            InputState.SoftGameplay => userInput.SoftGameplay.Movement.ReadValue<Vector2>(),
            _ => Vector2.zero,
        };
    }

}
