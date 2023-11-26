using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneState { None, Main, Town, Cave }

public class SceneLoader : Singleton<SceneLoader> {

	private SceneState currentSceneState;

	public void LoadScene(SceneState sceneState) {
		if (sceneState == SceneState.None) return;
		SceneManager.LoadScene(sceneState.ToString());
		currentSceneState = sceneState;
	}

}
