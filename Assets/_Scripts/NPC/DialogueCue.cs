using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCue : MonoBehaviour {

    [SerializeField] private LayerMask playerMask;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
		if ((1 << other.gameObject.layer | playerMask) == playerMask)
		spriteRenderer.enabled = true;
	}

    private void OnTriggerExit2D(Collider2D other) {
		if ((1 << other.gameObject.layer | playerMask) == playerMask)
        spriteRenderer.enabled = false;
    }

    public void DisableCue() {
        spriteRenderer.enabled = false;
    }

}
