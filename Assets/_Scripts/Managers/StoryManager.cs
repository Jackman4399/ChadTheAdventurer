using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StoryState {
	Introduction,
	CollectStrawberries,
	EncounterGoblin,
	GoblinSpare,
	GoblinKill,
	EmergencyQuest,
	IgnoreEmergencyQuest,
	ParticipateEmergencyQuest,
	WinEmergencyQuest,
	LoseEmergencyQuest,
	WinEmergencyQuestWithGoblin,
	LoseAllTogether
}

public class StoryManager : Singleton<StoryManager> {

	public event Action<StoryState> OnStoryChanged;

    private StoryState currentStoryState;
	[SerializeField] private StoryState initialStoryState;
    public StoryState CurrentStoryState => currentStoryState;
	public StoryState InitialStoryState => initialStoryState;

	[SerializeField] private string proceedName = "proceed";

	private Choice[] choices;
    public Choice[] Choices => choices;

	private Animator storyStateMachine;

	public bool isSkipping { get; private set; }

	protected override void Awake() {
		base.Awake();

		storyStateMachine = GetComponent<Animator>();

        currentStoryState = initialStoryState;

        choices = new Choice[] {
            new(ChoiceState.GoblinChoice),
            new(ChoiceState.EmergencyQuestChoice),
            new(ChoiceState.BossChoice),
        };

        foreach (var choice in choices)
        storyStateMachine.SetInteger(choice.ChoiceState.ToString(), choice.choiceNumber);

        isSkipping = true;
	}

	public void ChangeStoryState(StoryState storyState) {
		currentStoryState = storyState;
		OnStoryChanged?.Invoke(storyState);
	}

	public void Proceed() => storyStateMachine.SetTrigger(proceedName);

	public void MakeChoice(ChoiceState choiceState, int choiceNumber) {
		Choice choice = Array.Find(choices, c => c.ChoiceState == choiceState);
		storyStateMachine.SetInteger(choice.ChoiceState.ToString(), choiceNumber + 1);
	}

	public void DisableSkip() => isSkipping = false;

}
