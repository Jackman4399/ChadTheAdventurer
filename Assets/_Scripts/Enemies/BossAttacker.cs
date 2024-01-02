using System.Collections;
using System.Collections.Generic;
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

	public void MeleeAttack()
	{
		AudioManager.Instance.PlayOneShot("Boss_Melee");
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Collider2D colInfo = Physics2D.OverlapCircle(pos, meleeRange, attackMask);
		if (colInfo != null)
		{
			var player = GameObject.FindGameObjectWithTag("Player").transform.position;;
			var direction = (new Vector3(player.x, player.y, 0) - pos).normalized;
			colInfo.GetComponent<PlayerHealth>().TakeDamage(pushbackForce * direction, meleeDamage);
		}
	}

	private void FixedUpdate() {
		player_pos = GameObject.FindGameObjectWithTag("Hitbox").transform.position;
	}

	public void RangedAttack()
	{
		AudioManager.Instance.PlayOneShot("Boss_Ranged");

		float angle = Mathf.Atan2(player_pos.y, player_pos.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		Instantiate(shard, firePoint.position, rotation);
		//Damage logic is in Shard.cs
	}

	public void LaserAttack()
	{
		float angle = Mathf.Atan2(player_pos.y, player_pos.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		GameObject laser = Instantiate(laserEffect, laserPoint.position, rotation);
		StartCoroutine(ShootLaser(player_pos, laser));
	}

	IEnumerator ShootLaser(Vector2 direction, GameObject laser) {
		
		yield return new WaitForSeconds(1);
		AudioManager.Instance.PlayOneShot("Boss_Laser");
		// Get all objects hit by the laser
		RaycastHit2D[] hits = Physics2D.RaycastAll(laserPoint.position, direction);

		foreach (RaycastHit2D hit in hits) {
			var player = hit.transform.GetComponentInChildren<PlayerHealth>();
			if (player != null) {
				player.GetComponent<PlayerHealth>().TakeDamage(pushbackForce * direction, laserDamage);
			}
		}
		Destroy(laser);

	}

	void OnDrawGizmosSelected()
	{
		Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, meleeRange);
	}
}
