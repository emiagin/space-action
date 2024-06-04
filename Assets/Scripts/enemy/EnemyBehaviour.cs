using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
	public HeroBehaviour hero;
	public Pathfinding pathfinding;

	[SerializeField]
	private ParametersController parameters;

	List<Node> path = null;

	private void Awake()
	{
		//pathfinding = FindObjectOfType<Pathfinding>();
		//hero = FindObjectOfType<HeroBehaviour>();
		parameters.OnHealthNull += Death;
		//parameters.OnChangeHealth += LogController.Instance.Log_OnEnemyHealthChange;
	}

	~EnemyBehaviour()
	{
		parameters.OnHealthNull -= Death;
	}

	private void Death()
	{
		CollectorController.Instance.AddKill(1);
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

	void Update()
	{
		try
		{
			path = pathfinding.FindPath(transform.position, hero.CurrentPosition.position);
		}
		catch
		{

		}
		if (path != null && path.Count > 0)
		{
			Vector3 nextPosition = path[0].worldPosition;
			Vector3 direction = (nextPosition - transform.position).normalized;

			// Move enemy towards the next position
			transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * parameters.Speed);

			// Optionally: Rotate enemy to face the direction
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

			// Output current vector to hero and path points
			Debug.Log("Current vector to hero: " + (hero.CurrentPosition.position - transform.position).normalized);
			Debug.Log("Path points: " + string.Join(", ", path));
		}
	}
}
