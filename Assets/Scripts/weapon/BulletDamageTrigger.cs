using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageTrigger : MonoBehaviour
{
	private WeaponController parentWeapon;

	public void Init(WeaponController parentWeapon)
	{
		this.parentWeapon = parentWeapon;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{	
		//Debug.Log("Trigger enter");
		if (collision.tag == "Enemy")
		{
			parentWeapon.onEnemyDamaged?.Invoke(collision.GetComponent<EnemyController>());
			Destroy(gameObject);
		}
		else if(collision.tag == "Edge")
			Destroy(gameObject);
	}
}
