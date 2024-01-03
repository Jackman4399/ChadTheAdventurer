using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : MonoBehaviour
{
    [SerializeField, Min(0f)] private float speed = 8f;
    [SerializeField, Min(1)] private int damage = 1;

    private float pushbackForce = 100;

    public Rigidbody2D rb;

    private Vector2 player_pos;
    // Start is called before the first frame update
    void Start()
    {
        player_pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 direction = (player_pos - (Vector2)transform.position).normalized; // Calculate the direction towards the player
        rb.velocity = direction * speed; // Move in the direction of the player 
        StartCoroutine(Delete());
    }

    private void OnTriggerEnter2D(Collider2D other) {

        var player_pos = other.gameObject.transform.position;

        if(other.gameObject.tag.Equals("Player")) {

            other.GetComponentInChildren<PlayerHealth>().TakeDamage(pushbackForce * player_pos, damage);
            Destroy(gameObject);
        } 
        
         
    }

    IEnumerator Delete() {
        yield return new WaitForSeconds(15);
        if (gameObject != null) Destroy(gameObject);
    }
}
