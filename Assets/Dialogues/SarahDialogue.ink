INCLUDE GLOBAL.ink

-> main

=== main ===

~ CurrentStoryState = GetCurrentStoryState()

{ CurrentStoryState         :
- "Introduction"            : -> Introduction
- "CollectStrawberries"     : -> CollectStrawberries
- else                      : -> StoryStateNotRecognised
}

-> END

=== Introduction
#Sarah
The weather's nice today!

#Sarah
Are you short on cash again? You should pop by the guild...

#Sarah
Heard they got some new quests in today!
-> DONE

=== CollectStrawberries
#Sarah
The forest? Oh, just follow the road to the right.
-> DONE
