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
#Player
I don't really need to go out of town...

#Player
Perhaps I should ask the guild for any quests...
-> DONE

== CollectStrawberries
#Player
Am I ready to go out to the forest?

+ [Yes.]
    #Player
    Let's go.
    
    ~ ChangeInput("GameplayWithoutDash")
    ~ ChangeMenu("Gameplay")
    ~ ChangeNextScene()

+ [No.]
    #Player
    Maybe I'll take some time to prepare.
    
- -> DONE