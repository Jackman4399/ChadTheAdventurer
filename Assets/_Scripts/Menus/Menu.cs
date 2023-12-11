using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : MonoBehaviour {

    [SerializeField] private MenuState menuState;

    protected virtual void Awake() {
        MenuManager.Instance.OnMenuChanged += OnMenuChanged;
    }

    protected virtual void OnDestroy() {
        MenuManager.Instance.OnMenuChanged -= OnMenuChanged;
    }

    protected virtual void Start() => OnMenuChanged();

    private void OnMenuChanged() {
        bool enabled = MenuManager.Instance.Menus.Find(menu => menuState == menu.menuState).enabled;
        gameObject.SetActive(enabled);
    }

}
