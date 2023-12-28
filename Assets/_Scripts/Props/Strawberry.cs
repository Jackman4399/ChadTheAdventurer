using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : MonoBehaviour {
    
    [SerializeField] private LayerMask playerMask;

    [SerializeField] private string collectedName = "collected";

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if((1 << other.gameObject.layer | playerMask) != playerMask) return;

        animator.SetTrigger(collectedName);
        PlayerDataManager.Instance.AddStrawberry();
        AudioManager.Instance.PlayOneShot("Collect");
    }

    // For use within animation events
    private void Disable() {
        // Delete the object
        Destroy(gameObject);
    }

}
