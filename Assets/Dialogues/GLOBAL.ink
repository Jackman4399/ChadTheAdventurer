EXTERNAL GetCurrentStoryState()
EXTERNAL Proceed()
EXTERNAL ChangeScene(sceneName)
EXTERNAL ChangeNextScene()

VAR HaveVisited = false
VAR CurrentStoryState = ""

=== StoryStateNotRecognised
ERROR: STORY STATE NOT RECOGNISED
-> DONE