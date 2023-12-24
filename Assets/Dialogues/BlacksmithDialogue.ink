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

=== Introduction ===
{ HaveVisited   :
    #Blacksmith
    Well hello there! Welcome to my shop. I sell weapons to adventurers that thinks they got what it takes.
    
- else          :
    #Blacksmith
    Hello again!
}

#Blacksmith
Unfortunately, I'm not open to business at the moment. You can come back anytime soon, and hopefully I can get my shop up and running!

-> DONE

=== CollectHerbs ===

-> DONE