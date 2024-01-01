using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStateBehaviour : StateMachineBehaviour {

	[SerializeField] private StoryState storyState;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        StoryManager.Instance.ChangeStoryState(storyState);
	}

}
