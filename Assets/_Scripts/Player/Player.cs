using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Player : MonoBehaviour {

    protected UserInput userInput;

    protected Vector2 move;

    protected virtual void Awake() => userInput = InputManager.Instance.UserInput;


    protected virtual void Update() {
        switch (InputManager.Instance.CurrentInputState) {
            case InputState.SoftGameplay:
                move = userInput.SoftGameplay.Movement.ReadValue<Vector2>();
            break;

            case InputState.Gameplay:
                move = userInput.Gameplay.Movement.ReadValue<Vector2>();
            break;
        }
    }

}
