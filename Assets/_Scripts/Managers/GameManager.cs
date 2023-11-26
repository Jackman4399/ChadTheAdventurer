using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager> {
	
	[SerializeField] private InputState InitialiseInput;
	[SerializeField] private MenuState InitialiseMenu;
	[SerializeField] private SceneState InitialiseScene;

	private void Start() {
		InputManager.Instance.ChangeActionMap(InitialiseInput);
		MenuManager.Instance.ChangeMenu(InitialiseMenu);
		SceneLoader.Instance.LoadScene(InitialiseScene);
	}

}
