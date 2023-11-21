using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPlayerTest : Singleton<InitPlayerTest> {

    private void Start() {
        InputManager.Instance.ChangeActionMap(InputState.Gameplay);
    }

}
