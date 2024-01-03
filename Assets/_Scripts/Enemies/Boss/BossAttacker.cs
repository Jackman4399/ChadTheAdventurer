using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttacker : MonoBehaviour
{
    public int meleeDamage = 2;
	public int laserDamage = 2;

	public Vector3 attackOffset;
	public float meleeRange = 2f;
	public LayerMask attackMask;

	//Where projectiles fire from
	public Transform firePoint;
	
	//Where lasers fire from
	public Transform laserPoint;
	public GameObject shard;

	public GameObject laserEffect;

	private Vector2 player_pos;

	private float pushbackForce = 2000;

	private Animator animator;


	private void Awake() {
		animator = gameObject.GetComponent<Animator>();
	}

	public void MeleeAttack()
	{
		AudioManager.Instance.PlayOneShot("Boss_Melee");
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, meleeRange, attackMask);
		if (colInfo != null)
		{
			var player = colInfo.transform.position;
			var direction = (new Vector3(player.x, player.y, 0) - pos).normalized;
			colInfo.GetComponent<PlayerHealth>().TakeDamage(pushbackForce * direction, meleeDamage);
		}
	}

	private void FixedUpdate() {
		player_pos = GameObject.FindGameObjectWithTag("Player").transform.position;
	}

	public void RangedAttack()
	{
		AudioManager.Instance.PlayOneShot("Boss_Ranged");

		var direction = (player_pos - (Vector2) firePoint.position).normalized;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		Instantiate(shard, firePoint.position, rotation);
		//Damage logic is in Shard.cs
	}

	public void LaserAttack()
	{

		var direction = (player_pos - (Vector2) laserPoint.position).normalized;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		if (!animator.GetBool("OnCooldown")) Instantiate(laserEffect, laserPoint.position, rotation);
		StartCoroutine(Cooldown());
	}

    IEnumerator Cooldown()
    {
		animator.SetBool("OnCooldown", true);

        yield return new WaitForSeconds(5);

		animator.SetBool("OnCooldown", false);
    }
}
