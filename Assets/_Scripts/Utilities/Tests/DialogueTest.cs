using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTest : MonoBehaviour {

    private void Start() {
        InputManager.Instance.ChangeActionMap(InputState.Gameplay);
		MenuManager.Instance.ChangeMenu(Menu.Gameplay);
    }
	
}
