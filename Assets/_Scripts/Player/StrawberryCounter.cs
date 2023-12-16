using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberryCounter : MonoBehaviour
{
    [SerializeField] private int strawberries = 0;

    public void AddBerry(){

        strawberries += 1;

    }

    public int GetBerries(){
        return strawberries;
    }
}
