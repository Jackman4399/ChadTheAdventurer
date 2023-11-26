using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager> {
	
	[SerializeField] private InputState InitialiseInput;
	[SerializeField] private Menu InitialiseMenu;

	private void Start() {
		InputManager.Instance.ChangeActionMap(InitialiseInput);
		MenuManager.Instance.ChangeMenu(InitialiseMenu);
	}

}
