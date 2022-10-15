using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
	[SerializeField]
	private ParametersController parameters;

	private void Awake()
	{
		parameters.OnHealthNull += Death;
		parameters.OnChangeHealth += LogController.Instance.Log_OnEnemyHealthChange;
	}

	~EnemyBehaviour()
	{
		parameters.OnHealthNull -= Death;
	}

	private void Death()
	{
		Destroy(gameObject);
	}

	public void AttackTarget(ParametersController target)
	{
		target.Health -= parameters.Damage;
		Death();
	}

	public void TakeDamage(int damage)
	{
		parameters.Health -= damage;
	}
}
