using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : Singleton<StoryManager> {

	[SerializeField] private string currentStoryStateName = "currentStoryState", choiceName = "choice";
	[SerializeField] private int[] choices;

	private Animator storyStateMachine;

	public int currentStoryState { get; private set; }

	protected override void Awake() {
		base.Awake();

		storyStateMachine = GetComponent<Animator>();

		currentStoryState = 0;
		storyStateMachine.SetInteger(currentStoryStateName, currentStoryState);

		for (int i = 0; i < choices.Length; i++) {
			choices[i] = 0;
			storyStateMachine.SetInteger(choiceName + i, 0);
		}
	}

	public void Proceed() {
		storyStateMachine.SetInteger(currentStoryStateName, ++currentStoryState);
	}

	public void makeChoice(int choiceID, int choiceNumber) {
		choices[choiceID] = choiceNumber;
		storyStateMachine.SetInteger(choiceName + choiceID, choiceNumber);

		Proceed();
	}

}
