using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StoryState {
	Introduction,
	CollectHerbs,
	ExterminateGoblinTribe,
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

	[SerializeField] private StoryState initialStoryState, currentStoryState;
	public StoryState InitialStoryState => initialStoryState;
	public StoryState CurrentStoryState => currentStoryState;

	[SerializeField] private string proceedName = "proceed";

	[SerializeField] private Choice[] choices;
    public Choice[] Choices => choices;

	private Animator storyStateMachine;

	public bool isSkipping { get; private set; }

	protected override void Awake() {
		base.Awake();

		storyStateMachine = GetComponent<Animator>();

        if (choices.Length == 0) choices = new Choice[] {
            new(ChoiceState.GoblinChoice),
            new(ChoiceState.EmergencyQuestChoice),
            new(ChoiceState.BossChoice),
        };

		initialStoryState = StoryState.Introduction;
		currentStoryState = initialStoryState;

		isSkipping = true;

        foreach (var choice in choices) {
            choice.choiceNumber = 0;
            storyStateMachine.SetInteger(choice.ChoiceState.ToString(), 0);
        }
	}

	public void ChangeCurrentStoryState(StoryState storyState) {
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
