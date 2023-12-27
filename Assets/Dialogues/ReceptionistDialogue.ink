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
{ HaveVisited   :

    #Receptionist
    Hi! We meet again!

    #Receptionist
    Have you decided to take on the quest?

- else          :

    #Receptionist
    Hi! Welcome to the adventurer's guild!

    #Receptionist
    Excuse us for the cozy-looking environment this guild has, contrary to popular belief.

    #Receptionist
    Anyways, the guild is currently looking for someone to take on a simple gathering strawberries quest for a small reward, and a FREE meal!
}

#Receptionist
Do you want to take on this quest?

+ [Yes please.]
    ~ Proceed()

    #Receptionist
    Great! Let me fill you in on the details of the quest.
    
    #Receptionist
    The commision needs 8 strawberries found in the forest located outside of town. Pretty simple, right?
    
    #Receptionist
    We haven't decided on what reward you will receive yet, but expect it to be ready once you've completed the quest!
    
    #Receptionist
    Don't worry! Think of it as a surprise!
    
    #Receptionist
    Well then... hope you succeed on your quest!

+ [I'll come back later.]
    #Receptionist
    Alrighty! If you change your mind, feel free to come back to us anytime.
    
- -> DONE

=== CollectHerbs
#Receptionist
I hope you do well on your quest! We will reward you accordingly if you fulfill the objectives.

#Receptionist
In case you forgot, you need to find 8 strawberries scattered around the forest located outside of town.
-> DONE
