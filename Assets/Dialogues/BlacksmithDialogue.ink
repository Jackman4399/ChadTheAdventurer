INCLUDE GLOBAL.ink

-> main

=== main ===

~ CurrentStoryState = GetCurrentStoryState()

{ CurrentStoryState         :
- "Introduction"            : -> Introduction
- "CollectStrawberries"     : -> CollectStrawberries
- "GoblinSpare"             : -> AfterEncounteredGoblin
- "GoblinKill"              : -> AfterEncounteredGoblin
- "ClaimWeapon"             : -> ClaimWeapon
- else                      : -> StoryStateNotRecognised
}

-> END

=== NoBusiness

#Blacksmith
Sorry pal, but I'm out of stock at the moment. You can come back later, and hopefully I will have some goods to sell!

-> DONE

=== Introduction
{ HaveVisited   :

    #Blacksmith
    Hello again!
    
    
- else          :
    
    #Blacksmith
    Well hello there! Welcome to my shop. I sell weapons to the bravest of adventurers.
    
}

-> NoBusiness

-> DONE

=== CollectStrawberries

{ HaveVisited   :

    #Blacksmith
    Hello again!
    
    
- else          :
    
    #Blacksmith
    I see you've taken up the strawberry collection quest!
    
}

-> NoBusiness

-> DONE

=== AfterEncounteredGoblin

#Blacksmith
Hey pal! Glad to see you're not hurt from your quest.
    
#Blacksmith
You can go and claim your reward from the guild staff.

-> DONE

=== ClaimWeapon

#Blacksmith
Here you go! This is <>
{ GetChoice("GoblinChoice"):
- 1: 

a better weapon from your current one.

- 2: 

the best weapon the guild has.

}

Got <>
{ GetChoice("GoblinChoice"):
- 1: 

a better weapon

- 2: 

the best weapon

}
<> from the weapon shop!

{ GetChoice("GoblinChoice"):

- 2: 

    #Blacksmith
    The best weapon have more damage compared to any other weapon available right now.

}

#Blacksmith
Hope you enjoy using the new weapon you got!

#Chad
Thank you! I'll be sure to use it.

~ Proceed()

~ ChangeInput("SoftGameplay")
~ ChangeMenu("SoftGameplay")
~ ChangeNextScene(false)

-> DONE