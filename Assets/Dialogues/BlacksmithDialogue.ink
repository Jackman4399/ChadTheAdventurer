INCLUDE GLOBAL.ink

-> main

=== main ===

~ CurrentStoryState = GetCurrentStoryState()

{ CurrentStoryState :
- "Introduction"    : -> Introduction
- "CollectHerbs"    : -> Introduction
- else              : -> StoryStateNotRecognised
}

-> END

=== Introduction
{ HaveVisited   :

    #Blacksmith
    Hello again!
    
    
- else          :
    
    #Blacksmith
    Well hello there! Welcome to my shop. I sell weapons to adventurers that thinks they got what it takes.
    
}

#Blacksmith
Unfortunately, I'm not open to business at the moment. You can come back anytime soon, and hopefully I can get my shop up and running!

-> DONE