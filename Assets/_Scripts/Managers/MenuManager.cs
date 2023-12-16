using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public enum MenuState { Main, Gameplay, SoftGameplay, Dialogue, Win, Lose }

public class MenuManager : Singleton<MenuManager> {

    public event Action OnMenuChanged;

    private MenuData[] menus;
    public MenuData[] Menus => menus;

    protected override void Awake() {
        base.Awake();

        menus = new MenuData[] {
            new(MenuState.Main),
            new(MenuState.Gameplay),
            new(MenuState.SoftGameplay),
            new(MenuState.Dialogue),
            new(MenuState.Win),
            new(MenuState.Lose),
        };
    }

    public void ChangeMenu(MenuState menuState) {
        foreach (var menu in menus) {
            if (menu.MenuState == menuState) menu.enabled = true;
            else menu.enabled = false;
        }

        OnMenuChanged?.Invoke();
    }

    public Canvas FindMenu(MenuState menuState) {
        Canvas[] menus = FindObjectsByType<Canvas>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        return menus.Single(menu => menu.gameObject.name.Equals(menuState.ToString()));
    }

    public void EnableMenu(MenuState menuState) {
        Array.Find(menus, menu => menu.MenuState == menuState).enabled = true;
        OnMenuChanged?.Invoke();
    }

    public void DisableMenu(MenuState menuState) {
        Array.Find(menus, menu => menu.MenuState == menuState).enabled = false;
        OnMenuChanged?.Invoke();
    }

}