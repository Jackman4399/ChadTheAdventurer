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
It's a good weather today!

#Sarah
I heard the guild puts up a herb quest for an adventurer to pick up.

#Sarah
I wish I could be an adventurer someday, its seems fun to explore outside of this town!
-> DONE

=== CollectHerbs
#Sarah
I wish you on a safe journey with your herbs quest!
-> DONE
