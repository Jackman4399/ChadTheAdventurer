using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Player : MonoBehaviour {

    protected UserInput userInput;

    protected Vector2 move;

    protected virtual void Awake() => userInput = InputManager.Instance.userInput;


    protected virtual void Update() {
        move = userInput.Gameplay.Movement.ReadValue<Vector2>();
    }

}
