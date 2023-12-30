INCLUDE GLOBAL.ink

-> main

=== main ===
~ Proceed()

#Goblin
Ugh... I am so hurt right now...

#Chad
Hm... a goblin that could talk is rare nowadays.

#Goblin
Human! Help me with my misery!

#Chad #GoblinChoice
(Should I save it?)

+ [Spare the goblin.]

    #Chad
    I'll save you, goblin.

    #Goblin
    Oh, human! I will remember this deed. As a bonus, I'll grant you an ability. An ability to dash!

    You got an ability to dash!

    #CONTROLS
    Press SPACE while moving to dash

    #Goblin
    Now, human, I have to go somewhere else for now, and rest assured. I will not cause mischief to your settlement.

    #Human
    Okay, goblin. Have a nice trip.

+ [Kill it!]

    #Chad
    I'll kill you!

    #Goblin
    Nooo!

    \*Goblin died*

    #Chad
    Okay, time to head back!

- 

~ Proceed()
~ ChangeInput("SoftGameplay")
~ ChangeMenu("SoftGameplay")
~ ChangeNextScene()

-> END