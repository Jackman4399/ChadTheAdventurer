using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneState { None, Initialisation, Main, TownIntro, Forest, 
TownBeforeEmergencyQuest, TownEmergencyQuest, LoseEmergencyQuest, Cave }

public enum TransitionType { Cutscene, InScene, BetweenScenes }

public class SceneLoader : Singleton<SceneLoader> {

    public event Action<SceneState> OnSceneChanged;

    private Image crossfadeImage;

	[SerializeField] private SceneState currentSceneState;
    public SceneState CurrentSceneState => currentSceneState;

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

    public void ChangeScene(string sceneName, bool turnOnInput) {
        if (Enum.TryParse(sceneName, false, out SceneState sceneState)) ChangeScene(sceneState, turnOnInput);
        else Debug.LogWarning("Unable to parse given scene.");
    }

    public void ChangeNextScene(bool turnOnInput) => ChangeScene(currentSceneState + 1, turnOnInput);

    public void ChangeScene(SceneState sceneState, bool turnOnInput) {
        if (sceneState == SceneState.None) return; 
		Crossfade(sceneState, null, TransitionType.BetweenScenes, turnOnInput);
		currentSceneState = sceneState;
	}

    public void Crossfade(Action action, TransitionType transitionType) => 
    Crossfade(SceneState.None, action, transitionType, true);

    private void Crossfade(SceneState sceneState, Action action, TransitionType transitionType, bool turnOnInput) {
        StopAllCoroutines();
        StartCoroutine(CrossfadeCoroutine(sceneState, action, transitionType, turnOnInput));
    }

	private IEnumerator CrossfadeCoroutine(SceneState sceneState, Action action, 
    TransitionType transitionType, bool turnOnInput) {
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

		if (sceneState != SceneState.None) {
            yield return SceneManager.LoadSceneAsync(sceneState.ToString());
            OnSceneChanged?.Invoke(sceneState);
        }
        action?.Invoke();

		// Fade Out
        crossfadeImage.color = new Color(0, 0, 0, 1);

		while (crossfadeImage.color.a > 0) {
			crossfadeImage.color -= new Color(0, 0, 0, Time.deltaTime / transitionTime);
			yield return null;
		}

		crossfadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(crossfadeImage.color.a));

		if (turnOnInput) InputManager.Instance.ChangeInput(currentInputState);
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

}
