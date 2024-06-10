using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private float speed;
	private Vector2 direction;
	private WeaponController parentWeapon;

	private bool isMoving = false;

	public void Init(WeaponController parentWeapon, Vector2 dir, float speed)
	{
		this.parentWeapon = parentWeapon;
		this.speed = speed;
		this.direction = dir;

		isMoving = true;
	}

	private void FixedUpdate()
	{
		if (isMoving)
		{
			//Debug.Log($"Bullet pos = {transform.position}");
			transform.position += new Vector3(direction.x * speed, direction.y * speed, 0);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("Trigger enter");
		if (collision.CompareTag("Enemy"))
		{
			parentWeapon.onEnemyDamaged?.Invoke(collision.GetComponent<EnemyController>());
			Destroy(gameObject);
		}
		else if (collision.CompareTag("Edge"))
			Destroy(gameObject);
	}
}
