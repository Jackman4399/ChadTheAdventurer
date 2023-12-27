using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InputState { None, Menu, Gameplay, GameplayWithoutDash, SoftGameplay, Dialogue }

public class InputManager : Singleton<InputManager> {

    private UserInput userInput;
    public UserInput UserInput => userInput;

	private InputState currentInputState;
    public InputState CurrentInputState => currentInputState;

    protected override void Awake() {
        base.Awake();

        userInput = new UserInput();
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
