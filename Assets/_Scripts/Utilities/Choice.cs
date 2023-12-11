using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChoiceState { GoblinChoice, EmergencyQuestChoice, BossChoice }

[System.Serializable]
public class Choice {

    [SerializeField] private ChoiceState choiceState;
    public ChoiceState ChoiceState => choiceState;
    public int choiceNumber;

    public Choice(ChoiceState choiceState) {
        this.choiceState = choiceState;
    }

}
