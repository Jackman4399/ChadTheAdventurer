using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTest : Singleton<DialogueTest> {

    private void Start() {
        InputManager.Instance.ChangeActionMap(InputState.Gameplay);
		MenuManager.Instance.ChangeMenu(MenuState.Gameplay);
    }
	
}
