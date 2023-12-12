using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneState { None, Main, Town, Cave }

public class SceneLoader : Singleton<SceneLoader> {

    private Image crossfadeImage;

	private SceneState currentSceneState;

    protected override void Awake() {
        base.Awake();

        crossfadeImage = GetComponentInChildren<Image>();
		crossfadeImage.color = new Color(0, 0, 0, 0);
    }

	public void ChangeScene(SceneState sceneState) {
		if (sceneState == SceneState.None) return;
		SceneManager.LoadScene(sceneState.ToString());
		currentSceneState = sceneState;
	}

}
