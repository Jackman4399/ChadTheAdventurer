using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : Menu {

	public void Play() {
        InputManager.Instance.ChangeInput(InputState.SoftGameplay);
        MenuManager.Instance.ChangeMenu(MenuState.SoftGameplay);
        SceneLoader.Instance.ChangeScene(SceneState.TownIntro);
    }

    public void Quit() {
        Application.Quit();
    }

}
