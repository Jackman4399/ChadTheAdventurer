using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseMenu : Menu {

    public void BackToMainMenu() {
        MenuManager.Instance.ChangeMenu(MenuState.Main);
        SceneLoader.Instance.ChangeScene(SceneState.Main, true);
    }

}
