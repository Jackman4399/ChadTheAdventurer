using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum MenuState { Main, Crossfade, Gameplay, Dialogue, Win, Lose }

public class MenuManager : Singleton<MenuManager> {

	[SerializeField] private GameObject menusPrefab;
    [SerializeField] private Canvas[] menus;

	private Image crossfadeImage;
	[SerializeField] private float transitionTime = 1;

    protected override void Awake() {
        base.Awake();

		menusPrefab = GameObject.Find(menusPrefab.name) ? 
		GameObject.Find(menusPrefab.name) : Instantiate(menusPrefab);
		DontDestroyOnLoad(menusPrefab);

		menus = menusPrefab.GetComponentsInChildren<Canvas>();
		foreach (var menu in menus) menu.gameObject.SetActive(false);

		crossfadeImage = FindMenu(MenuState.Crossfade).GetComponentInChildren<Image>();
		crossfadeImage.color = new Color(0, 0, 0, 0);
    }

    public void ChangeMenu(MenuState menuState) {
        foreach (var menu in menus) {
            if (menu.name.Equals(menuState.ToString())) menu.gameObject.SetActive(true);
            else menu.gameObject.SetActive(false);
        }
    }

    public Canvas FindMenu(MenuState menuState) {
        return Array.Find(menus, menu => menu.name.Equals(menuState.ToString()));
    }

    public void EnableMenu(MenuState menuState) => Array.Find(menus, menu => 
	menu.name.Equals(menuState.ToString())).gameObject.SetActive(true);

    public void DisableMenu(MenuState menuState) => Array.Find(menus, menu => 
	menu.name.Equals(menuState.ToString())).gameObject.SetActive(false);

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