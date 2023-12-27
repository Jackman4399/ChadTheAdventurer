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
{ HaveVisited   :

    #Blacksmith
    Hello again!
    
    
- else          :
    
    #Blacksmith
    Well hello there! Welcome to my shop. I sell weapons to the bravest of adventurers.
    
}

#Blacksmith
Sorry pal, I'm out of stock at the moment. You can come back later, and hopefully I will have some goods to sell!

-> DONE

=== CollectStrawberries

{ HaveVisited   :

    #Blacksmith
    Hello again!
    
    
- else          :
    
    #Blacksmith
    I've see you've taken up on the collecting strawberries quest!
    
}

#Blacksmith
Sorry pal, I'm out of stock at the moment. You can come back later, and hopefully I will have some goods to sell!

-> DONE