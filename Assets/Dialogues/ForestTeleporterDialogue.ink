INCLUDE GLOBAL.ink

-> main

=== main ===

~ CurrentStoryState = GetCurrentStoryState()

{ CurrentStoryState         :
- "Introduction"            : -> Introduction
- "CollectStrawberries"     : -> CollectStrawberries
- "GoblinSpare"             : -> NoForest
- "GoblinKill"              : -> NoForest
- "ClaimWeapon"             : -> NoForest
- "EmergencyQuest"          : -> NoForest
- else                      : -> StoryStateNotRecognised
}

-> END

=== Introduction
#Chad
I don't really need to go out of town...

#Chad
Perhaps I should ask the guild for any quests...
-> DONE

=== CollectStrawberries
#Chad
Am I ready to go out to the forest?

+ [Yes.]
    #Chad
    Let's go.
    
    ~ ChangeInput("GameplayWithoutDash")
    ~ ChangeMenu("Gameplay")
    ~ ChangeNextScene(true)

+ [No.]
    #Chad
    Maybe I'll take some more time to prepare.
    
- -> DONE

=== NoForest

#Chad
I have no business outside of town for now.

-> DONE