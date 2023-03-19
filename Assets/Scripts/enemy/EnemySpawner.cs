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
		//StartSpawn(spawns);
	}

	public void StartSpawn(SpotSpawnInfo[] spawnInfos)
	{
		for(int i = 0; i < spawnInfos.Length; i++)
		{
			spawnPoints[i].StartSpawn(spawnInfos[i], targetHero, enemyPrefab);
		}
	}
}