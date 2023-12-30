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
    private int strawberriesCount = 7;
    public int StrawberriesCount => strawberriesCount;

    public void AddStrawberry() {
        strawberriesCount++;
        if (strawberriesCount == strawberriesNeeded) OnStrawberriesEnough?.Invoke();
    }

}
