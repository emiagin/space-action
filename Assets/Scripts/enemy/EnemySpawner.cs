using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField]
	private Transform targetHero;
	[SerializeField]
	private Transform enemyPrefab;
	[SerializeField]
	private SpotSpawn[] spawnPoints;

	public bool SpawnerWorkComplete
	{
		get
		{
			if (!_startSpawn)
				return false;

			bool result = true;
			//string log = "";
			foreach (var spawn in spawnPoints)
			{
				result &= spawn.SpawnComplete;
				//log += $" {spawn.SpawnComplete}";
			}
			//Debug.Log($"~complete: {log}");

			if (result && _startSpawn)
				_startSpawn = false;

			return result;
		}
	}
	private bool _startSpawn = false;

	public void StartSpawn(SpotSpawnInfo[] spawnInfos)
	{
		_startSpawn = true;
		for (int i = 0; i < spawnInfos.Length; i++)
		{
			//Debug.Log($"---- Spot {i}: Start Spawn");
			spawnPoints[i].StartSpawn(spawnInfos[i], targetHero, enemyPrefab);
		}
	}
}