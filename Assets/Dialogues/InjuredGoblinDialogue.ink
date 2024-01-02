INCLUDE GLOBAL.ink

-> main

=== main ===
~ Proceed()

#Goblin
Ugh... This isn't good...

#Chad
Hm... a goblin that could talk is rare nowadays.

#Goblin
Human! Please.. help me! I mean no harm! I just happened to be attacked by some wild animals...

#Goblin
You are the 3rd one passing by today... I can't hold on for much longer.

#Chad #GoblinChoice
(Should I save it?)

+ [Save the goblin.]

    #Chad
    Sure, I guess.
    
    ...

    #Goblin
    Oh, human! I will remember this deed. As a bonus, I'll bestow you this charm from my clan. It'll make you faster!

    You got "God of Wind's charm"!

    #CONTROLS
    Press SPACE while moving to dash

    #Goblin
    Now, human, I have to go somewhere else for now, and rest assured. I will not cause mischief to your settlement.

    #Human
    Okay, goblin. Make it back home, you hear me?!

+ [Kill it!]

    #Chad
    Nah, you look valuable...

    #Goblin
    Nooo! Please!

    \*Goblin died*

    #Chad
    Okay, time to head back!

- 

~ Proceed()

~ ChangeInput("SoftGameplay")
~ ChangeMenu("SoftGameplay")
~ ChangeNextScene(true)

-> END