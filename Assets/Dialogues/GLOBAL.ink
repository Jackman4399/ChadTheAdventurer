EXTERNAL GetCurrentStoryState()
EXTERNAL Proceed()
EXTERNAL ChangeNextScene()
EXTERNAL ChangeScene(sceneName)
EXTERNAL ChangeInput(inputName)
EXTERNAL ChangeMenu(menuName)

VAR HaveVisited = false
VAR CurrentStoryState = ""

VAR StrawberriesCount = 0
VAR StrawberriesNeeded = 0

=== StoryStateNotRecognised
#ERROR
ERROR: STORY STATE NOT RECOGNISED
-> DONE