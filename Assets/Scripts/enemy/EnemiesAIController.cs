using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesAIController : MonoBehaviour
{
	public Pathfinding pathfinding;
	public HeroController heroController;

	private List<Tuple<EnemyAI, int>> enemies = new List<Tuple<EnemyAI, int>>();
	private bool movePermissionAll;

	public Action<EnemyAI, EnemyAI> OnEnemiesStartCollision, OnEnemiesEndCollision;
	public Action<EnemyAI> OnPlayerCollision;

	private void Start()
	{
		OnEnemiesStartCollision += EnemiesStartCollision;
		OnEnemiesEndCollision += EnemiesEndCollision;
		OnPlayerCollision += PlayerCollision;

		var enemiesArr = FindObjectsOfType<EnemyAI>();
		for(int i = 0; i < enemiesArr.Length; i++)
		{
			enemiesArr[i].StartTargeting(pathfinding, this, heroController.CurrentPosition);
			enemies.Add(new Tuple<EnemyAI, int>(enemiesArr[i], 0));
		}

		movePermissionAll = SetMovePermissionAll(true);
	}

	~EnemiesAIController()
	{
		OnEnemiesStartCollision -= EnemiesStartCollision;
		OnEnemiesEndCollision -= EnemiesEndCollision;
		OnPlayerCollision -= PlayerCollision;
	}

	private bool SetMovePermissionAll(bool on)
	{
		if(pathfinding != null && pathfinding.IsPathfinderReady)
		{
			foreach (var enemy in enemies)
				enemy.Item1.SetPermissionToMove(on);
			return true;
		}

		return false;
	}

	private void Update()
	{
		foreach (var enemy in enemies)
			enemy.Item1.SetTarget(heroController.CurrentPosition);
	}

	private void EnemiesStartCollision(EnemyAI enemyCall, EnemyAI enemyHit)
	{
		int enemyCallIndex = enemies.FindIndex(x => x.Item1.Equals(enemyCall));
		int enemyHitIndex = enemies.FindIndex(x => x.Item1.Equals(enemyHit));

		bool isEnemyCallHigher = enemyCall.FullPathSize >= enemyHit.FullPathSize;
		int newEnemyCallPriority = isEnemyCallHigher ? enemies[enemyHitIndex].Item2 + 1 : enemies[enemyHitIndex].Item2 - 1;
		int newEnemyHitPriority = isEnemyCallHigher ? newEnemyCallPriority - 1 : newEnemyCallPriority + 1;

		enemies[enemyCallIndex] = new Tuple<EnemyAI, int>(enemies[enemyCallIndex].Item1, newEnemyCallPriority);
		enemies[enemyHitIndex] = new Tuple<EnemyAI, int>(enemies[enemyHitIndex].Item1, newEnemyHitPriority);

		enemyHit.SetPermissionToMove(newEnemyHitPriority > newEnemyCallPriority);
		enemyCall.SetPermissionToMove(newEnemyHitPriority < newEnemyCallPriority);
	}

	private void EnemiesEndCollision(EnemyAI enemyCall, EnemyAI enemyHit)
	{
		//Debug.Log("Exit collision");
		enemyCall.SetPermissionToMove(true);
		enemyHit.SetPermissionToMove(true);
	}

	private void PlayerCollision(EnemyAI enemy)
	{
		//Debug.Log($"Switch off {enemy.name}");
		int index = enemies.FindIndex(x => x.Item1.Equals(enemy));
		enemies[index].Item1.SetPermissionToMove(false);
		enemies[index].Item1.SetInteractions(false);
		enemies.RemoveAt(index);
	}
}
