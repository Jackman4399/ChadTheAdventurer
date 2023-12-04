using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float strength = 30, delay = 0.1f;

    public UnityEvent OnBegin, OnDone;

    public void PlayFeedback(GameObject sender){

        Debug.Log("GOT HIT");

        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset() {
        
        yield return new WaitForSeconds(delay);
        rb.velocity = Vector3.zero;
        OnDone?.Invoke();
        Debug.Log("Done reset");
    }
}
