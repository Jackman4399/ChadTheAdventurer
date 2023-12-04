using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceedScript : MonoBehaviour
{   

    public LevelLoaderScript levelLoader;

    private void Start() {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoaderScript>();
    }

    private void OnTriggerEnter2D(Collider2D trigger) {

        //Will only trigger if the tag matches
        if (trigger.gameObject.layer.Equals("Player")) {
            
            levelLoader.LoadNextScene();

            //Uncomment and comment out the above to manually select scene
            //sceneLoader.LoadCertainScene(1);

        }

    }
}
