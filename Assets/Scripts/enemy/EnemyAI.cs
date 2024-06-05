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
	private float _fullPathSize = 0;
	public float FullPathSize => _fullPathSize;

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
		//Debug.Log($"Set move {this.name} to {canMove}");
		this.canMove = canMove;
	}

	public void SetInteractions(bool on)
	{
		this.GetComponent<CircleCollider2D>().enabled = on;
	}

	private void Update()
	{
		if (!canMove)
			return;

		//Debug.Log($"Moving target {target.position}");
		if(pathfinding!= null && target != null)
			path = pathfinding.FindPath(transform.position, target.position);
		
		if (path != null && path.Count > 0)
		{
			_fullPathSize = pathfinding.PathSize;
			_nextNodePosition = path[0].worldPosition;
			MoveTowardsTarget(_nextNodePosition);
		}
	}

	private void MoveTowardsTarget(Vector3 nextPosition)
	{
		//Vector3 direction = (nextPosition - transform.position).normalized;
		transform.position = Vector3.MoveTowards(transform.position, nextPosition, Time.deltaTime * parameters.Speed);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Enemy")
		{
			//Debug.Log("Enemies hit enter");
			var enemyHit = collision.collider.GetComponentInChildren<EnemyAI>();
			enemiesAIController.OnEnemiesStartCollision?.Invoke(this, enemyHit);
		}
		else if (collision.collider.tag == "Player")
		{
			//Debug.Log("Player hit enter");
			enemiesAIController.OnPlayerCollision?.Invoke(this);
		}
	}

	private void OnCollisionExitD(Collision2D collision)
	{
		if (collision.collider.tag == "Enemy")
		{
			//Debug.Log("Enemies hit exit");
			var enemyHit = collision.collider.GetComponentInChildren<EnemyAI>();
			enemiesAIController.OnEnemiesEndCollision?.Invoke(this, enemyHit);
		}
	}
}
