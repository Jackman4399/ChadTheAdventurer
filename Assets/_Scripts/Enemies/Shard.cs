using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shard : MonoBehaviour
{
    [SerializeField, Min(10f)] private float speed = 20f;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: Make it so that it works for 2D top down (360 degrees) rather than left-right
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        Debug.Log(other.name + " hit!");

        

        Destroy(gameObject);

    }
}
