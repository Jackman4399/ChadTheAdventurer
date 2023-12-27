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
    Hi! We meet again.

    #Receptionist
    I assume you've made up your mind.

- else          :

    #Receptionist
    Hi! Welcome to the adventurer's guild.

    #Receptionist
    Excuse us for the cozy-looking environment this guild has, contrary to popular belief.

    #Receptionist
    Anyways, the guild is currently looking for someone to take on a simple gathering strawberries quest, and a reward will be given.
}

#Receptionist
Do you want to take on this quest?

+ [Yes please.]
    ~ Proceed()

    #Receptionist
    Great! Let me fulfil you on the details of the quest.
    
    #Receptionist
    You will need to find 7 strawberries scattered around the forest located outside of town. Pretty simple, right?
    
    #Receptionist
    We haven't yet decide on what reward you will receive, but expect it to be ready once you've done the quest!
    
    #Receptionist
    We will make sure it is equivalent in terms of value to the quest you're taking.
    
    #Receptionist
    Finally, we hope you succeed in your quest!

+ [I'll give it some time to think.]
    #Receptionist
    That's alright. If you changed your mind, feel free to come back to us anytime.
    
- -> DONE

=== CollectHerbs
#Receptionist
I hope you do well on your quest! We will reward you accordingly if you fulfil the objectives.

#Receptionist
In case you forgot, you will need to find 7 strawberries scattered around the forest located outside of town.
-> DONE
