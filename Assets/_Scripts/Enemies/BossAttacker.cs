using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacker : MonoBehaviour
{
    public int meleeDamage = 1;
	public int rangedDamage = 2;

	public Vector3 attackOffset;
	public float attackRange = 1f;
	public LayerMask attackMask;

	public Transform firePoint;

	public GameObject shard;

	public void MeleeAttack()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			//Player should take melee damage here
			//colInfo.GetComponent<PlayerHealth>().TakeDamage(meleeDamage);
		}
	}

	public void RangedAttack()
	{
		//TODO: Make the bullet shoot towards the player
		Instantiate(shard, firePoint.position, firePoint.rotation);
		//If player gets hit by projectile, take damage
		//colInfo.GetComponent<PlayerHealth>().TakeDamage(rangedDamage);
	}

	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}
}
