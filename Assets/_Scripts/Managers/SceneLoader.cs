using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneState { None, Main, TownIntro, Cave }

public class SceneLoader : Singleton<SceneLoader> {

    private Image crossfadeImage;

	[SerializeField] private SceneState currentSceneState;

    [SerializeField, Tooltip("Transition time between different places in the same scene in seconds.")] 
    private float transitionTime = .25f;
    [SerializeField, Tooltip("Transition time between different scenes in seconds.")] 
    private float transitionTimeBetweenScenes = 1;

    protected override void Awake() {
        base.Awake();

        crossfadeImage = GetComponentInChildren<Image>();
		crossfadeImage.color = new Color(0, 0, 0, 0);

        try {
            currentSceneState = Enum.Parse<SceneState>(SceneManager.GetActiveScene().name);
        } catch (Exception) { Debug.LogWarning("Unable to parse current scene."); }
    }

    public void ChangeNextScene() {
        Scene nextScene = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1);
        
        SceneState nextSceneState = SceneState.None;

        try {
            nextSceneState = Enum.Parse<SceneState>(nextScene.name);
        } catch (Exception) { Debug.LogWarning("Unable to parse next scene."); }

        ChangeScene(nextSceneState);
    }

    public void ChangeScene(SceneState sceneState) {
		if (sceneState == SceneState.None) return;
		Crossfade(sceneState);
		currentSceneState = sceneState;
	}

    public void Crossfade(SceneState sceneState) => StartCoroutine(CrossfadeCoroutine(sceneState));

	private IEnumerator CrossfadeCoroutine(SceneState sceneState) {
        float transitionTime = sceneState == SceneState.None ? this.transitionTime : transitionTimeBetweenScenes;

		InputState currentInputState = InputManager.Instance.CurrentInputState;

		InputManager.Instance.ChangeInput(InputState.None);

		// Fade In
		while (crossfadeImage.color.a < 1) {
			crossfadeImage.color += new Color(0, 0, 0, Time.deltaTime / transitionTime);
			yield return null;
		}

		crossfadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(crossfadeImage.color.a));

		if (sceneState != SceneState.None) SceneManager.LoadScene(sceneState.ToString());

		// Fade Out
		while (crossfadeImage.color.a > 0) {
			crossfadeImage.color -= new Color(0, 0, 0, Time.deltaTime / transitionTime);
			yield return null;
		}

		crossfadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(crossfadeImage.color.a));

		InputManager.Instance.ChangeInput(currentInputState);
	}

}
