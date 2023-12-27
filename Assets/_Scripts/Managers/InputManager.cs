using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InputState { None, Menu, Gameplay, GameplayWithoutDash, SoftGameplay, Dialogue }

public class InputManager : Singleton<InputManager> {

    private UserInput userInput;
    public UserInput UserInput => userInput;

	[SerializeField] private InputState currentInputState;
    public InputState CurrentInputState => currentInputState;

    protected override void Awake() {
        base.Awake();

        userInput = new UserInput();
    }

    public void ChangeInput(string inputName) {
        if (Enum.TryParse(inputName, false, out InputState inputState)) ChangeInput(inputState);
        else Debug.LogWarning("Unable to parse given input.");
    }

    public void ChangeInput(InputState inputState) {
        userInput.Disable();

        InputActionMap actionMap = null;
		if (inputState != InputState.None) {
			actionMap = 
        	Array.Find(userInput.asset.actionMaps.ToArray(), 
			actionMap => actionMap.name.Equals(inputState.ToString()));
		}

		currentInputState = inputState;

        actionMap?.Enable();
    }

}
