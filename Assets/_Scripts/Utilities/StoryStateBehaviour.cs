using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStateBehaviour : StateMachineBehaviour {

	[SerializeField] private StoryState storyState;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (StoryManager.Instance.isSkipping) {
			if (StoryManager.Instance.initialStoryState != storyState) StoryManager.Instance.Proceed();
			else StoryManager.Instance.DisableSkip();
		} else StoryManager.Instance.ChangeCurrentStoryState(storyState);
	}

}
