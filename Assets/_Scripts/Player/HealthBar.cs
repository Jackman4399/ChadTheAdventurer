using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public int healthPoints;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public PlayerHealth playerHealth;


    private void Start() {
        UpdateHealth();
    }

    //Will sync the hp with the player's
    private void UpdateHealth(){
        double bias = 0.5;
        double temp = playerHealth.GetCurrentHP();
        healthPoints = (int) Math.Round(temp/20 + bias);
    }

    void FixedUpdate() {

        UpdateHealth();

        if(healthPoints > numOfHearts) {
            healthPoints = numOfHearts;
        }
        
        for (int i = 0; i < hearts.Length; i++){
            
            if(healthPoints == 0) {
                hearts[i].sprite = emptyHeart;
            } else {

                if(i < healthPoints) {
                    hearts[i].sprite = fullHeart;
                } else {
                    hearts[i].sprite = emptyHeart;
                }

                if (i < numOfHearts)
                {
                    hearts[i].enabled = true;
                } else {
                    hearts[i].enabled = false;
                }
            }
        }

    }
}
