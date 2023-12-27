using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Barrier : MonoBehaviour
{

    private StrawberryCounter counter;

    //Change this to alter how many berries are needed for the invisible wall to go away
    private int berriesNeeded = 1;

    private void Start() {
        counter = FindObjectOfType<StrawberryCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        if(counter != null && counter.GetBerries() > berriesNeeded) {
            
            Destroy(gameObject);

        }
    }
}
