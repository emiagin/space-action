using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageTrigger : MonoBehaviour
{
	[SerializeField]
	private GameObject bullet;
	[SerializeField]
	private int damage = 1;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log("Trigger enter");
		if (collision.tag == "Enemy")
		{
			//collision.GetComponent<EnemyAI>().TakeDamage(damage);
			Destroy(bullet);
		}
		else if(collision.tag == "Edge")
			Destroy(bullet);
	}
}
