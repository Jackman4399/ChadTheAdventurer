using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceedScript : MonoBehaviour
{   

    public SceneLoaderScript sceneLoader;

    private void Start() {
        sceneLoader = GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<SceneLoaderScript>();
    }

    private void OnTriggerEnter2D(Collider2D trigger) {

        //Will only trigger if the tag matches
        if (trigger.gameObject.layer.Equals("Player")) {
            
            sceneLoader.LoadNextScene();

            //Uncomment and comment out the above to manually select scene
            //sceneLoader.LoadCertainScene(1);

        }

    }
}
