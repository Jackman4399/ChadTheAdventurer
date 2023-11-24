using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager> {

	[SerializeField] private bool isTesting;

	private void Start() {
		if (isTesting) return;

		InputManager.Instance.ChangeActionMap(InputState.Menu);
		MenuManager.Instance.ChangeMenu(Menu.Main);
	}

}
