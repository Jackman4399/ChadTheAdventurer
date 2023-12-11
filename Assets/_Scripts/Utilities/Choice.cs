using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChoiceState { GoblinChoice, EmergencyQuestChoice, BossChoice }

[System.Serializable]
public class Choice {

    public ChoiceState choiceState;

}
