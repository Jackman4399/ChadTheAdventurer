INCLUDE GLOBAL.ink

-> main

=== main ===

~ CurrentStoryState = GetCurrentStoryState()

{ CurrentStoryState         :
- "Introduction"            : -> Introduction
- "CollectStrawberries"     : -> CollectStrawberries
- "GoblinSpare"             : -> GoblinSpare
- "GoblinKill"              : -> GoblinKill
- "ClaimWeapon"             : -> ClaimWeapon
- "EmergencyQuest"          : -> EmergencyQuest
- else                      : -> StoryStateNotRecognised
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
    The commission needs 8 strawberries found in the forest located outside of town. Pretty simple, right?
    
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

=== CollectStrawberries

#Receptionist
I hope you do well on your quest! We will reward you accordingly if you fulfill the objectives.

#Receptionist
In case you forgot, you need to find 8 strawberries scattered around the forest located outside of town.

-> DONE

=== GoblinSpare

#Receptionist
Hello! Welcome back!
    
#Receptionist
How was the quest? Did you manage to complete it?
    
#Chad
I did.
    
#Receptionist
Great! Now, if you could hand over the strawberries, we will reward you with a weapon that's better than your current one.
    
#Chad
Oh, that's the reward? Awesome!
    
#Chad
Here are the strawberries.
    
#Receptionist
...
    
#Receptionist
Okay! I counted 8 strawberries. You can claim the weapon at the blacksmith's weapon shop.
    
#Chad
Okay! Thank you.
    
~ Proceed()

-> DONE

=== GoblinKill

#Receptionist
Hello! Welcome back!
    
#Receptionist
How was the quest? Did you managed to complete it?
    
#Chad
I did. I also killed a random goblin I encountered.

#Chad
Here's its head.

#Receptionist
Oh my! This is the Green Tribes Leader! And whoever killed it would get the best weapon that we have!

#Receptionist
Okay then, let me fetch a suitable reward to give you for completing both the strawberry gathering quest and the bounty on the goblin.
    
#Chad
That's Awesome!

#Receptionist
...
    
#Receptionist
Okay! I can confirm there are 8 strawberries. You can claim the weapon at the blacksmith's weapon shop.
    
#Chad
Okay! Thank you.

~ Proceed()

-> DONE

=== ClaimWeapon

#Receptionist
Hello again!

#Receptionist
You can claim the reward which is <>
{ 
- GetChoice("GoblinChoice") == 1: a better
- GetChoice("GoblinChoice") == 2: the best
} 
<> weapon at the blacksmith's weapon shop.

-> DONE

=== EmergencyQuest

{ HaveVisited   :

#Receptionist
Hello again! I hope you made up your mind.

- else          :

#Receptionist
Hello, Chad! The guild needs your help. We need you to investigate an ominous energy emitting from the nearby cave... 

}

#Receptionist #EmergencyQuestChoice
Would you like to take on this emergency quest?

+ [I am not strong enough...]

#Receptionist
Oh no! Alright then... Hopefully nothing bad happens.. We'll try to find someone else.

#Chad
I'm sorry but I can't do it...

~ Proceed()

~ ChangeInput("Menu")
~ ChangeMenu("Lose")
~ ChangeScene("LoseEmergencyQuest", true)

+ [I am ready!]

#Chad
Let's do this!

#Receptionist
Okay! We will briefly explain what you will need to do.

~ Proceed()

~ ChangeScene("Cave", false)

+ [I still need some time to think.]

#Receptionist
That's okay. If you've made a decision you can come back and talk to me, but make a decison soon! Who knows how dangerous that ominous presence is...

- -> DONE