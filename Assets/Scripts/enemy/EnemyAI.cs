using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	private Transform target;
	private Pathfinding pathfinding;
	private EnemiesAIController enemiesAIController;

	[SerializeField]
	private ParametersController parameters;

	private List<Node> path = null;

	private Vector3 _nextNodePosition;
	private Vector3 _currentNodePosition;
	public Vector3 CurrentNodePosition => _currentNodePosition;

	private bool canMove;

	

	public void Init(Pathfinding pathfinding, EnemiesAIController enemiesAIController, Transform target)
	{
		this.enemiesAIController = enemiesAIController;
		this.pathfinding = pathfinding;
		SetTarget(target);
	}

	public void SetTarget(Transform target)
	{
		this.target = target;
	}

	public void SetPermissionToMove(bool canMove)
	{
		this.canMove = canMove;
	}

	private void Update()
	{
		if (!canMove)
			return;

		if(pathfinding!= null && target != null)
			path = pathfinding.FindPath(transform.position, target.position);
		
		if (path != null && path.Count > 0)
		{
			_nextNodePosition = path[0].worldPosition;
			MoveTowardsTarget(_nextNodePosition);
		}
	}

	private void MoveTowardsTarget(Vector3 nextPosition)
	{
		//Vector3 direction = (nextPosition - transform.position).normalized;
		transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * parameters.Speed);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Enemy")
		{
			enemiesAIController.OnEnemiesCollision?.Invoke(this, collision);
		}
	}
}
