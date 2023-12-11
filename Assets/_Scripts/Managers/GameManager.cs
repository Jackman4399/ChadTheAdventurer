using UnityEngine;

public class GameManager : PersistentSingleton<GameManager> {
	
    [SerializeField] private InputState InitialiseInput;
    [SerializeField] private MenuState InitialiseMenu;
    [SerializeField] private SceneState InitialiseScene;

    private void Start() {
        InputManager.Instance.ChangeInput(InitialiseInput);
        MenuManager.Instance.ChangeMenu(InitialiseMenu);
        SceneLoader.Instance.ChangeScene(InitialiseScene);
    }

}
