using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneState { None, Main, TownIntro, CollectStrawberries, Cave }

public enum TransitionType { Cutscene, InScene, BetweenScenes }

public class SceneLoader : Singleton<SceneLoader> {

    private Image crossfadeImage;

	[SerializeField] private SceneState currentSceneState;

    [Header("Transition Times")]
    [SerializeField, Tooltip("Transition time when a player triggers a cutscene in seconds.")] 
    private float transitionTimeToCutscene = .75f; 
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

    public void ChangeScene(string sceneName) {
        if (Enum.TryParse(sceneName, false, out SceneState sceneState)) ChangeScene(sceneState);
        else Debug.LogWarning("Unable to parse given scene.");
    }

    public void ChangeNextScene() => ChangeScene(currentSceneState + 1);

    public void ChangeScene(SceneState sceneState) {
		if (sceneState == SceneState.None) return;
		Crossfade(sceneState, null, TransitionType.BetweenScenes);
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

    public void Crossfade(Action action, TransitionType transitionType) => 
    Crossfade(SceneState.None, action, transitionType);

    private void Crossfade(SceneState sceneState, Action action, TransitionType transitionType) {
        StopAllCoroutines();
        StartCoroutine(CrossfadeCoroutine(sceneState, action, transitionType));
    }

	private IEnumerator CrossfadeCoroutine(SceneState sceneState, Action action, TransitionType transitionType) {
        float transitionTime = transitionType switch {
            TransitionType.Cutscene => transitionTimeToCutscene,
            TransitionType.InScene => transitionTimeInScene,
            TransitionType.BetweenScenes => transitionTimeBetweenScenes,
            _ => 1,
        };

		var currentInputState = InputManager.Instance.CurrentInputState;

		InputManager.Instance.ChangeInput(InputState.None);

		// Fade In
        crossfadeImage.color = new Color(0, 0, 0, 0);

		while (crossfadeImage.color.a < 1) {
			crossfadeImage.color += new Color(0, 0, 0, Time.deltaTime / transitionTime);
			yield return null;
		}

		crossfadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(crossfadeImage.color.a));

		if (sceneState != SceneState.None) SceneManager.LoadScene(sceneState.ToString());
        action?.Invoke();

		// Fade Out
        crossfadeImage.color = new Color(0, 0, 0, 1);

		while (crossfadeImage.color.a > 0) {
			crossfadeImage.color -= new Color(0, 0, 0, Time.deltaTime / transitionTime);
			yield return null;
		}

		crossfadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(crossfadeImage.color.a));

		InputManager.Instance.ChangeInput(currentInputState);
	}

}
