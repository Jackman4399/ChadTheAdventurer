using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : MonoBehaviour
{
    [SerializeField, Min(10f)] private float speed = 20f;
    [SerializeField, Min(1)] private int damage = 1;

    private float pushbackForce = 1000;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: Make it so that it works for 2D top down (360 degrees) rather than left-right
        Vector2 player_pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        rb.velocity = player_pos * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        Debug.Log(other.name + " hit!");

        var player_pos = other.gameObject.transform.position;

        if(other.gameObject.tag.Equals("Player")) {

            other.GetComponent<PlayerHealth>().TakeDamage(pushbackForce * player_pos, damage);
                
        }

        Destroy(gameObject);

    }
}
