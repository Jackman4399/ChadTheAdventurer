using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttacker : MonoBehaviour
{
    public int meleeDamage = 2;
	public int laserDamage = 2;

	public Vector3 attackOffset;
	public float attackRange = 1f;
	public LayerMask attackMask;

	//Where projectiles fire from
	public Transform firePoint;
	
	//Where lasers fire from
	public Transform laserPoint;
	public GameObject shard;

	public GameObject laserEffect;

	private float pushbackForce = 1000;

	public void MeleeAttack()
	{
		//Play sound here
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
		if (colInfo != null)
		{
			var player = LocatePlayer();
			var direction = (new Vector3(player.x, player.y, 0) - pos).normalized;
			colInfo.GetComponent<PlayerHealth>().TakeDamage(pushbackForce * direction, meleeDamage);
		}
	}

	public void RangedAttack()
	{
		//Play sound here
		Vector2 player_pos = LocatePlayer();

		float angle = Mathf.Atan2(player_pos.y, player_pos.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		Instantiate(shard, firePoint.position, rotation);
		//Damage logic is in Shard.cs
	}

	public void LaserAttack()
	{
		var direction = LocatePlayer();
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		Instantiate(laserEffect, laserPoint.position, rotation);

		//Play sound here
		StartCoroutine(ShootLaser(direction));
	}

	IEnumerator ShootLaser(Vector2 direction) {
		
		yield return new WaitForSeconds(1);

		RaycastHit2D info = Physics2D.Raycast(laserPoint.position, direction);

		if(info) {

			var player = info.transform.GetComponentInChildren<PlayerHealth>();
			if (player != null) {
        		player.GetComponent<PlayerHealth>().TakeDamage(pushbackForce * direction, laserDamage);
			}
		}

	}

	private Vector2 LocatePlayer() {

		Vector2 player_pos = GameObject.FindGameObjectWithTag("Player").transform.position;
		return player_pos;

	}

	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
	}
}
