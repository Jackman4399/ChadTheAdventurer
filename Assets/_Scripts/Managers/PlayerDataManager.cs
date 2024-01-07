using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
using UnityEngine;

public class PlayerDataManager : Singleton<PlayerDataManager> {

    public event Action OnStrawberriesEnough;

    //Change this to alter how many berries are needed for the invisible wall to go away
    [SerializeField] private int strawberriesNeeded = 8;
    public int StrawberriesNeeded => strawberriesNeeded;
    private int strawberriesCount;
    public int StrawberriesCount => strawberriesCount;

    private void Start() {
        SceneLoader.Instance.OnSceneChanged += OnSceneChanged;
    }

    private void OnDestroy() {
        SceneLoader.Instance.OnSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(SceneState sceneState) {
        if (sceneState == SceneState.Main) strawberriesCount = 0;
    }

    public void AddStrawberry() {
        strawberriesCount++;
        if (strawberriesCount == strawberriesNeeded) OnStrawberriesEnough?.Invoke();
    }

}
