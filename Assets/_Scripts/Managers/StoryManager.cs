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
    ClaimWeapon,
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
    public StoryState CurrentStoryState => currentStoryState;
	[SerializeField] private StoryState initialStoryState;
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
        storyStateMachine.Play(initialStoryState.ToString());

        choices = new Choice[] {
            new(ChoiceState.GoblinChoice, 0),
            new(ChoiceState.EmergencyQuestChoice, 0),
            new(ChoiceState.BossChoice, 0),
        };

        if (initialStoryState == StoryState.GoblinSpare) choices[0].choiceNumber = 1;
        else if (initialStoryState == StoryState.GoblinKill) choices[0].choiceNumber = 2;

        // Change goblin choice here
        else if (initialStoryState > StoryState.EncounterGoblin) choices[0].choiceNumber = 1;

        isSkipping = true;
	}

    private void Start() {
        SceneLoader.Instance.OnSceneChanged += OnSceneChanged;
    }

    private void OnDestroy() {
        SceneLoader.Instance.OnSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(SceneState sceneState) {
        if (sceneState == SceneState.Main) {
            currentStoryState = StoryState.Introduction;
            storyStateMachine.Play(currentStoryState.ToString());

            foreach (var choice in choices) {
                choice.choiceNumber = 0;
                storyStateMachine.SetInteger(choice.ChoiceState.ToString(), 0);
            }
        }
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

    public int GetChoice(string choiceName) {
        if (!Enum.TryParse(choiceName, false, out ChoiceState choiceState)) {
            Debug.LogWarning("Cannot parse the given choice.");
            return 0;
        } else return GetChoice(choiceState);
        
    }

    public int GetChoice(ChoiceState choiceState) {
        return Array.Find(choices, c => c.ChoiceState == choiceState).choiceNumber;
    }

	public void DisableSkip() => isSkipping = false;

}
