EXTERNAL GetCurrentStoryState()
EXTERNAL GetChoice(choiceName)
EXTERNAL Proceed()
EXTERNAL ChangeNextScene(turnOnInput)
EXTERNAL ChangeScene(sceneName, turnOnInput)
EXTERNAL ChangeInput(inputName)
EXTERNAL ChangeMenu(menuName)

VAR HaveVisited = false
VAR CurrentStoryState = ""

VAR StrawberriesCount = 0
VAR StrawberriesNeeded = 0

=== StoryStateNotRecognised
#ERROR
ERROR: STORY STATE { ~GetCurrentStoryState() } NOT RECOGNISED
-> DONE