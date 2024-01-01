using UnityEngine;

public class GameManager : PersistentSingleton<GameManager> {
	
    [SerializeField] private InputState InitialiseInput;
    [SerializeField] private MenuState InitialiseMenu;

    private void Start() {
        InputManager.Instance.ChangeInput(InitialiseInput);
        MenuManager.Instance.ChangeMenu(InitialiseMenu);
    }

}
