using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Menu { Main, Gameplay, Dialogue, Win, Lose }

public class MenuManager : Singleton<MenuManager> {

    [SerializeField] private Canvas[] menus;

    protected override void Awake() {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneChanged;
    }

    private void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene scene, LoadSceneMode loadSceneMode) {
        menus = FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        menus = menus.Where(menu => menu.renderMode != RenderMode.WorldSpace).ToArray();
    }

    public void ChangeMenu(Menu menu) {
        foreach (var menuCanvas in menus) {
            if (menuCanvas.name.Equals(menu.ToString() + "Menu")) menuCanvas.gameObject.SetActive(true);
            else menuCanvas.gameObject.SetActive(false);
        }
    }

    public Canvas FindMenu(Menu menu) {
        return Array.Find(menus, menuCanvas => 
		menuCanvas.name.Equals(menu.ToString() + "Menu"));
    }

    public void EnableMenu(Menu menu) => Array.Find(menus, menuCanvas => 
	menuCanvas.name.Equals(menu.ToString() + "Menu")).gameObject.SetActive(true);

    public void DisableMenu(Menu menu) => Array.Find(menus, menuCanvas => 
	menuCanvas.name.Equals(menu.ToString() + "Menu")).gameObject.SetActive(false);

}