using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public enum MenuState { Main, Crossfade, Gameplay, Dialogue, Win, Lose }

public class MenuManager : Singleton<MenuManager> {

    [SerializeField] private List<Canvas> menus;

	private Image crossfadeImage;
	[SerializeField] private float transitionTime = .25f;

    protected override void Awake() {
        base.Awake();

        menus = new List<Canvas>();

		crossfadeImage = GetComponentInChildren<Image>();
		crossfadeImage.color = new Color(0, 0, 0, 0);

        SceneManager.sceneLoaded += OnSceneChanged;
    }

    private void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene scene, LoadSceneMode loadSceneMode) {
        menus = FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        menus = menus.Where(menu => menu.renderMode != RenderMode.WorldSpace).ToList();
    }

    public void ChangeMenu(MenuState menuState) {
        foreach (var menu in menus) menu.gameObject.SetActive(false);

        StartCoroutine(ChangeMenuCoroutine(menuState));
    }

    private IEnumerator ChangeMenuCoroutine(MenuState menuState) {
        Canvas menu = null;

        while (menu == null) {
            menu = menus.Find(m => m.name.Equals(menuState.ToString() + "Menu"));
            yield return null;
        }

        menu.gameObject.SetActive(true);
    }

    public Canvas FindMenu(MenuState menuState) {
        return menus.Find(menu => menu.name.Equals(menuState.ToString() + "Menu"));
    }

    public void EnableMenu(MenuState menuState) => menus.Find(menu =>
    menu.name.Equals(menuState.ToString() + "Menu")).gameObject.SetActive(true);

    public void DisableMenu(MenuState menuState) => menus.Find(menu => 
    menu.name.Equals(menuState.ToString() + "Menu")).gameObject.SetActive(false);

	public void Crossfade(SceneState sceneState) => StartCoroutine(CrossfadeCoroutine(sceneState));

	private IEnumerator CrossfadeCoroutine(SceneState sceneState) {
		InputState currentInputState = InputManager.Instance.currentInputState;

		InputManager.Instance.ChangeActionMap(InputState.None);
		EnableMenu(MenuState.Crossfade);

		// Fade In
		while (crossfadeImage.color.a < 1) {
			crossfadeImage.color += new Color(0, 0, 0, Time.deltaTime / transitionTime);
			yield return null;
		}

		crossfadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(crossfadeImage.color.a));

		if (sceneState != SceneState.None) SceneLoader.Instance.LoadScene(sceneState);

		// Fade Out
		while (crossfadeImage.color.a > 0) {
			crossfadeImage.color -= new Color(0, 0, 0, Time.deltaTime / transitionTime);
			yield return null;
		}

		crossfadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(crossfadeImage.color.a));

		DisableMenu(MenuState.Crossfade);
		InputManager.Instance.ChangeActionMap(currentInputState);
	}

}