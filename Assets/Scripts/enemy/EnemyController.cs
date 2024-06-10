using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	[SerializeField]
	private DataController dataController;
	[SerializeField]
	private EnemyAI enemyAI;

	private EnemyParameters enemyParameters;

	public Action<EnemyController> onEnemyDamaged;
	public Action<EnemyController> onEnemyDestroyed;

	private void Awake()
	{
		enemyParameters = dataController.LoadEnemyData();
		enemyAI.Init(enemyParameters);
	}

	public void TakeDamage(int damage)
	{
		enemyParameters.CurrentHealth -= damage;
		onEnemyDamaged?.Invoke(this);

		if (enemyParameters.CurrentHealth == 0)
		{
			onEnemyDestroyed?.Invoke(this);
			Destroy(gameObject);
		}
	}
}