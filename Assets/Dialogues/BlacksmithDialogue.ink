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
Sorry pal, but I'm out of stock at the moment. You can come back later, and hopefully I might have some goods to sell!

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
Hey pal! Glad to see you're back all safe and sound!
    
#Blacksmith
I suggest you check in with the guild.

-> DONE

=== ClaimWeapon

#Blacksmith
Here you go! This is <>
{ GetChoice("GoblinChoice"):
- 1: 

a slight upgrade from your current one.

- 2: 

the best weapon these old hands could forge.

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
    This weapon could rival even the toughest armour, slicing through steel like paper.

}

#Blacksmith
Take care of your new weapon!

#Chad
Thank you! I'll be sure to use it well.

~ Proceed()

~ ChangeInput("SoftGameplay")
~ ChangeMenu("SoftGameplay")
~ ChangeNextScene(false)

-> DONE