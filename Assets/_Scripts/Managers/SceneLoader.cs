using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneState { None, Main, TownIntro, Cave }

public class SceneLoader : Singleton<SceneLoader> {

    private Image crossfadeImage;
    public Image CrossfadeImage => crossfadeImage;

	[SerializeField] private SceneState currentSceneState;

    [SerializeField, Tooltip("Transition time between different places in the same scene in seconds.")] 
    private float transitionTimeInScene = .25f;
    [SerializeField, Tooltip("Transition time between different scenes in seconds.")] 
    private float transitionTimeBetweenScenes = 1;

    protected override void Awake() {
        base.Awake();

        crossfadeImage = GetComponentInChildren<Image>();
		crossfadeImage.color = new Color(0, 0, 0, 0);

        if (!Enum.TryParse(SceneManager.GetActiveScene().name, false, out currentSceneState)) 
        Debug.LogWarning("Unable to parse current scene.");
    }

    public void ChangeNextScene() {
        var nextScene = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1);

        if (Enum.TryParse(nextScene.name, false, out SceneState nextSceneState)) ChangeScene(nextSceneState);
        else Debug.LogWarning("Unable to parse next scene.");
    }

    public void ChangeScene(string sceneName) {
        if (Enum.TryParse(sceneName, false, out SceneState sceneState)) ChangeScene(sceneState);
        else Debug.LogWarning("Unable to parse given scene.");
    }

    public void ChangeScene(SceneState sceneState) {
		if (sceneState == SceneState.None) return;
		Crossfade(sceneState, null);
		currentSceneState = sceneState;
	}

    // For use in timeline
    public void ChangeBackgroundFade(float alpha) {
        if (alpha > 1 || alpha < 0) return;

        crossfadeImage.color = new Color(
            crossfadeImage.color.r,
            crossfadeImage.color.g,
            crossfadeImage.color.b,
            alpha
        );
    }

    public void Crossfade(Action action) => Crossfade(SceneState.None, action);

    private void Crossfade(SceneState sceneState, Action action) => 
    StartCoroutine(CrossfadeCoroutine(sceneState, action));

	private IEnumerator CrossfadeCoroutine(SceneState sceneState, Action action) {
        float transitionTime = sceneState == SceneState.None ? transitionTimeInScene : transitionTimeBetweenScenes;

		var currentInputState = InputManager.Instance.CurrentInputState;

		InputManager.Instance.ChangeInput(InputState.None);

		// Fade In
		while (crossfadeImage.color.a < 1) {
			crossfadeImage.color += new Color(0, 0, 0, Time.deltaTime / transitionTime);
			yield return null;
		}

		crossfadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(crossfadeImage.color.a));

		if (sceneState != SceneState.None) SceneManager.LoadScene(sceneState.ToString());
        action?.Invoke();

		// Fade Out
		while (crossfadeImage.color.a > 0) {
			crossfadeImage.color -= new Color(0, 0, 0, Time.deltaTime / transitionTime);
			yield return null;
		}

		crossfadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(crossfadeImage.color.a));

		InputManager.Instance.ChangeInput(currentInputState);
	}

}
