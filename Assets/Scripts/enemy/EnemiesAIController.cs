using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesAIController : MonoBehaviour
{
	public Pathfinding pathfinding;
	public HeroBehaviour heroBehaviour;

	private List<EnemyAI> enemies;
	private bool movePermissionAll;

	public Action<EnemyAI, Collider2D> OnEnemiesCollision;

	private void Start()
	{
		OnEnemiesCollision += EnemiesCollision;

		enemies = FindObjectsOfType<EnemyAI>().ToList();
		foreach (var enemy in enemies)
			enemy.Init(pathfinding, this, heroBehaviour.CurrentPosition);
		movePermissionAll = SetMovePermissionAll(true);
	}

	~EnemiesAIController()
	{
		OnEnemiesCollision -= EnemiesCollision;
	}

	private bool SetMovePermissionAll(bool on)
	{
		if(pathfinding != null && pathfinding.IsPathfinderReady)
		{
			foreach (var enemy in enemies)
				enemy.SetPermissionToMove(on);
			return true;
		}

		return false;
	}

	private void Update()
	{
		
	}

	private void EnemiesCollision(EnemyAI enemy, Collider2D collision)
	{

	}
}