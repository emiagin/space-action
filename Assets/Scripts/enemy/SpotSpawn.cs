using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Pathfinding;

public class SpotSpawn : MonoBehaviour
{
	private SpotSpawnInfo _info;

	private int _countSpawned = 0;
	private Transform _target;
	private Transform _prefab;

	[HideInInspector]
	public bool SpawnComplete = false;

	public void StartSpawn(SpotSpawnInfo info, Transform target, Transform prefab)
	{
		StopCoroutine("WaitSpawnDelay");
		SpawnComplete = false;
		_info = info;
		_countSpawned = 0;
		_prefab = prefab;
		_target = target;
		Spawn();
	}

	private void Spawn()
	{
		if (_countSpawned == _info.EnemyCount)
		{
			EndSpawn();
			return;
		}

		//Debug.Log($"------- Spawn count={_countSpawned + 1}");
		var go = Instantiate(_prefab, transform);
		go.parent = transform;
		go.localPosition = Vector3.zero;
		//go.GetComponent<AIDestinationSetter>().target = _target;

		var name = go.GetComponent<ParametersController>().MainName;
		name = name + "_" + _countSpawned.ToString();

		_countSpawned++;
		StartCoroutine("WaitSpawnDelay");
	}

	private void EndSpawn()
	{
		SpawnComplete = true;
		//Debug.Log($"------- End Spawn");
	}

	IEnumerator WaitSpawnDelay()
	{
		yield return new WaitForSeconds(_info.Delay);
		Spawn();
	}
}

[System.Serializable]
public class SpotSpawnInfo
{
	public int EnemyCount; //стоит >0 = спавнит ровно столько, независимо от таймера
	public float Delay;
	public int Timer; //стоит таймер = спавнит пока таймер не истечет

	public SpotSpawnInfo(int _enemyCount, float _delay, int _timer)
	{
		EnemyCount = _enemyCount;
		Delay = _delay;
		Timer = _timer;
	}
}