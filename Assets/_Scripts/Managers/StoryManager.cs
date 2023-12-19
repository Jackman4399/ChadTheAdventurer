using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StoryState {
	Introduction,
	CollectHerbs,
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

	[SerializeField] private StoryState initialStoryState;
    private StoryState currentStoryState;
	public StoryState InitialStoryState => initialStoryState;
	public StoryState CurrentStoryState => currentStoryState;

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

	public void makeChoice(ChoiceState choiceState, int choiceNumber) {
		Choice choice = Array.Find(choices, c => c.ChoiceState == choiceState);
		storyStateMachine.SetInteger(choice.ChoiceState.ToString(), choiceNumber + 1);

		Proceed();
	}

	public void DisableSkip() => isSkipping = false;

}
