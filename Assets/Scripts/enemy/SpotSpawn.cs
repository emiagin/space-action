using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SpotSpawn : MonoBehaviour
{
	private int _countSpawned = 0;
	private int _countNeed = 0;
	private float _delay = 0f;
	private Transform _target;
	private Transform _prefab;

	public void StartSpawn(int count, float delay, Transform target, Transform prefab)
	{
		//Debug.Log("Spawn start");
		_countSpawned = 0;
		_countNeed = count;
		_delay = delay;
		_prefab = prefab;
		_target = target;
		Spawn();
	}

	private void Spawn()
	{
		if (_countSpawned == _countNeed)
		{
			EndSpawn();
			return;
		}

		//Debug.Log($"Spawn count={_countSpawned + 1}");
		var go = Instantiate(_prefab, transform);
		go.parent = transform;
		go.localPosition = Vector3.zero;
		go.GetComponent<AIDestinationSetter>().target = _target;

		var name = go.GetComponent<ParametersController>().MainName;
		name = name + "_" + _countSpawned.ToString();

		_countSpawned++;
		StartCoroutine("WaitSpawnDelay");
	}

	private void EndSpawn()
	{
		//Debug.Log("Spawn end");
	}

	IEnumerator WaitSpawnDelay()
	{
		yield return new WaitForSeconds(_delay);
		Spawn();
	}
}
