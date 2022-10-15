using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField]
	private Transform targetHero;
	[SerializeField]
	private Transform enemyPrefab;
	[SerializeField]
	private SpotSpawn[] spawnPoints;

	private void Awake()
	{
		spawnPoints[0].StartSpawn(10, 0.5f, targetHero, enemyPrefab);
		spawnPoints[1].StartSpawn(5, 1f, targetHero, enemyPrefab);
		spawnPoints[2].StartSpawn(7, 1.5f, targetHero, enemyPrefab);
		spawnPoints[3].StartSpawn(3, 2f, targetHero, enemyPrefab);
	}
}