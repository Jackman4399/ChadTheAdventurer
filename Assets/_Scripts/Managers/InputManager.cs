using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum InputState { None, Menu, Gameplay, Dialogue }

public class InputManager : Singleton<InputManager> {

    public UserInput userInput { get; private set; }

	public InputState currentInputState { get; private set; }

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
