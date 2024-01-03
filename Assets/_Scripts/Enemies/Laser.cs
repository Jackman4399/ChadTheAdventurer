using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private Vector2 player_pos;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player_pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 direction = (player_pos - (Vector2)transform.position).normalized;
        animator = gameObject.GetComponentInChildren<Animator>();
        StartCoroutine(ShootLaser(direction));
    }

    IEnumerator ShootLaser(Vector2 direction) {

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


		AudioManager.Instance.PlayOneShot("Boss_Laser");
        if(animator != null) animator.SetTrigger("Shoot");

        yield return new WaitForSeconds(1);
		// Get all objects hit by the laser
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction);

		foreach (RaycastHit2D hit in hits) {
			var player = hit.transform.GetComponentInChildren<PlayerHealth>();
			if (player != null) {
				player.GetComponent<PlayerHealth>().TakeDamage(1000 * direction, 2);
			}
		}

        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
	}


}
