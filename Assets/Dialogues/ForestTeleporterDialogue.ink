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
#Player
I have no business going outside of town.

#Player
Perhaps ask the guild staff for a quest?
-> DONE

== CollectHerbs
#Player
Am I ready to go out to the forest?

+ [Yes.]
    ~ ChangeNextScene()

+ [No.]
    #Player
    Maybe I'll take some time to prepare.
    

- <> -> DONE