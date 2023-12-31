using System;
using UnityEngine;

public enum ChoiceState { GoblinChoice, EmergencyQuestChoice, BossChoice }

[Serializable]
public class Choice {

    [SerializeField] private ChoiceState choiceState;
    public ChoiceState ChoiceState => choiceState;
    
    public int choiceNumber;

    public Choice(ChoiceState choiceState, int choiceNumber) {
        this.choiceState = choiceState;
        this.choiceNumber = choiceNumber;
    }

}
