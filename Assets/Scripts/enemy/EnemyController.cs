using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField]
	private int maxHealth = 1;
	private int currentHealth;

	public Action<EnemyController> onEnemyDamaged;
	public Action<EnemyController> onEnemyDestroyed;

	private void Start()
	{
		currentHealth = maxHealth;
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		onEnemyDamaged?.Invoke(this);

		if (currentHealth <= 0)
		{
			onEnemyDestroyed?.Invoke(this);
			Destroy(gameObject);
		}
	}
}