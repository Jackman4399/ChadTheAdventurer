using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour
{

    private bool collected = false;
    
    [SerializeField] private Animator animator;

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.layer == LayerMask.NameToLayer("Player")){

            animator.SetTrigger("Collected");
            
            if(!collected) {

                collected = true;
                other.gameObject.GetComponent<StrawberryCounter>().AddBerry();

            }

            AudioManager.Instance.PlayOneShot("Collect");

        }

    }

    private void Update() {
        // Check if the animation is playing
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Collected"))
        {
            // Check if the animation is finished
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                // Delete the object
                Destroy(gameObject);
            }
        }
    }

}
