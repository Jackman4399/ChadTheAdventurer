INCLUDE GLOBAL.ink

-> main

=== main ===

~ CurrentStoryState = GetCurrentStoryState()

{ CurrentStoryState :
- "Introduction"    : -> Introduction
- "CollectHerbs"    : -> CollectHerbs
- else              : -> StoryStateNotRecognised
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

=== CollectHerbs
#Sarah
The forest? Oh, just follow this path!
-> DONE
