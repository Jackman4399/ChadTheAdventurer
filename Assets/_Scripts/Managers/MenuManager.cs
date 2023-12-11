using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public enum MenuState { Main, Gameplay, Dialogue, Win, Lose }

public class MenuManager : Singleton<MenuManager> {

    public event Action OnMenuChanged;

    [SerializeField] private List<MenuData> menus;
    public List<MenuData> Menus => menus;

	private Image crossfadeImage;
	[SerializeField] private float transitionTime = .25f;

    protected override void Awake() {
        base.Awake();

        menus = new List<MenuData>() {
            new(MenuState.Main),
            new(MenuState.Gameplay),
            new(MenuState.Dialogue),
            new(MenuState.Win),
            new(MenuState.Lose),
        };

		crossfadeImage = GetComponentInChildren<Image>();
		crossfadeImage.color = new Color(0, 0, 0, 0);
    }

    public void ChangeMenu(MenuState menuState) {
        foreach (var menu in menus) {
            if (menu.menuState == menuState) menu.enabled = true;
            else menu.enabled = false;
        }

        OnMenuChanged?.Invoke();
    }

    public Canvas FindMenu(MenuState menuState) {
        Canvas[] menus = FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        return menus.Single(menu => menu.gameObject.name.Equals(menuState.ToString()));
    }

    public void EnableMenu(MenuState menuState) {
        menus.Find(menu => menu.menuState == menuState).enabled = true;
        OnMenuChanged?.Invoke();
    }

    public void DisableMenu(MenuState menuState) {
        menus.Find(menu => menu.menuState == menuState).enabled = false;
        OnMenuChanged?.Invoke();
    }

	public void Crossfade(SceneState sceneState) => StartCoroutine(CrossfadeCoroutine(sceneState));

	private IEnumerator CrossfadeCoroutine(SceneState sceneState) {
		InputState currentInputState = InputManager.Instance.currentInputState;

		InputManager.Instance.ChangeInput(InputState.None);

		// Fade In
		while (crossfadeImage.color.a < 1) {
			crossfadeImage.color += new Color(0, 0, 0, Time.deltaTime / transitionTime);
			yield return null;
		}

		crossfadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(crossfadeImage.color.a));

		if (sceneState != SceneState.None) SceneLoader.Instance.ChangeScene(sceneState);

		// Fade Out
		while (crossfadeImage.color.a > 0) {
			crossfadeImage.color -= new Color(0, 0, 0, Time.deltaTime / transitionTime);
			yield return null;
		}

		crossfadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(crossfadeImage.color.a));

		InputManager.Instance.ChangeInput(currentInputState);
	}

}