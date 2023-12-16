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


            StartCoroutine(DestroyAfterSeconds(1));

        }

    }

    IEnumerator DestroyAfterSeconds(int seconds)
    {
        // Wait for the specified number of seconds
        yield return new WaitForSeconds(seconds);

        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
    }

}
