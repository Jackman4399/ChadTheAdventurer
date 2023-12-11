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

	[SerializeField] private string proceedName = "proceed", choiceName = "choice";

	[SerializeField] private int[] choices;

	private Animator storyStateMachine;

	public bool isSkipping { get; private set; }

	protected override void Awake() {
		base.Awake();

		storyStateMachine = GetComponent<Animator>();

		initialStoryState = StoryState.Introduction;
		currentStoryState = initialStoryState;

		isSkipping = true;

		for (int i = 0; i < choices.Length; i++) {
			choices[i] = 0;
			storyStateMachine.SetInteger(choiceName + i, 0);
		}
	}

	public void ChangeCurrentStoryState(StoryState storyState) {
		currentStoryState = storyState;
		OnStoryChanged?.Invoke(storyState);
	}

	public void Proceed() => storyStateMachine.SetTrigger(proceedName);

	public void makeChoice(int choiceID, int choiceNumber) {
		choices[choiceID] = choiceNumber;
		storyStateMachine.SetInteger(choiceName + choiceID, choiceNumber);

		Proceed();
	}

	public void DisableSkip() => isSkipping = false;

}
