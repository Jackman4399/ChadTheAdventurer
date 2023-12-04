using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : Singleton<PlayerTest> {

    private void Start() {
        InputManager.Instance.ChangeActionMap(InputState.Gameplay);
    }

}
