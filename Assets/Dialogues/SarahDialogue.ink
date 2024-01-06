INCLUDE GLOBAL.ink

-> main

=== main ===

~ CurrentStoryState = GetCurrentStoryState()

{ CurrentStoryState             :
- "Introduction"                : -> Introduction
- "CollectStrawberries"         : -> CollectStrawberries
- "GoblinSpare"                 : -> BeforeEmergencyQuest
- "GoblinKill"                  : -> BeforeEmergencyQuest
- "ClaimWeapon"                 : -> ClaimWeapon
- "EmergencyQuest"              : -> EmergencyQuest
- else                          : -> StoryStateNotRecognised
}

-> END

=== Introduction
#Sarah
The weather's nice today!

#Sarah
Are you short on cash again? You should pop by the guild...

#Sarah
I heard they got some new quests in today!
-> DONE

=== CollectStrawberries
#Sarah
The forest? Oh, just follow the road to the right.
-> DONE

=== BeforeEmergencyQuest

#Sarah
Welcome back! How was the quest?

#Sarah
You can talk with the guild staff about the completion of your quest.

-> DONE

=== ClaimWeapon

#Sarah
You get a weapon as a reward? That's great!

#Sarah
You can claim it at the blacksmith's weapon shop.

-> DONE

=== EmergencyQuest

#Sarah
Oh no! The town could be in great danger...

#Sarah
Where is an adventurer that is brave enough to dwelve into the caves when you need them!

-> DONE