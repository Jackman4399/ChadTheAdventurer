using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager> {
	
	private void Start() {
		InputManager.Instance.ChangeActionMap(InputState.Menu);
		MenuManager.Instance.ChangeMenu(Menu.Main);
	}

	public void LoadScene() {
		
	}

}
