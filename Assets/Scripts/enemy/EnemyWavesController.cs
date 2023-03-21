using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesController : MonoBehaviour
{
	[SerializeField]
	private EnemySpawner _enemySpawner;

	private Level _level;
	private int _currentWaveIndex;
	private bool _startWaveSpawners = false;

	public void StartEnemiesSpawnerOnLevel(Level level)
	{
		//Debug.Log("Start Waves Spawner");
		_level = level;
		_currentWaveIndex = -1;
		_startWaveSpawners = true;
		NextWave();
	}

	private void NextWave()
	{
		_currentWaveIndex++;

		if (_currentWaveIndex == _level.EnemiesWaves.Length)
		{
			StopWaves();
			return;
		}
		//Debug.Log($"New Wave {_currentWaveIndex}");

		_enemySpawner.StartSpawn(_level.EnemiesWaves[_currentWaveIndex].Infos);
	}

	private void Update()
	{
		if (_startWaveSpawners && _enemySpawner.SpawnerWorkComplete)
			NextWavePrepare();
	}

	private void NextWavePrepare()
	{
		//Debug.Log($"Next Wave Prepare time ={_level.EnemiesWaves[_currentWaveIndex].Timer} sec");
		StartCoroutine("WaitWaveTime");
	}

	private void StopWaves()
	{
		//Debug.Log($"Stop Waves Spawner");
		_startWaveSpawners = false;
	}

	IEnumerator WaitWaveTime()
	{
		yield return new WaitForSeconds(_level.EnemiesWaves[_currentWaveIndex].Timer);
		NextWave();
	}
}

[System.Serializable]
public class Level
{
	public string LevelName;
	public int LevelTime;
	public EnemiesWave[] EnemiesWaves; 
}
[System.Serializable]
public struct EnemiesWave
{
	public SpotSpawnInfo InfoAll;
	public int SpotsCount;
	public int Timer; //не переходит к следующей волне, пока не истечет таймер

	public SpotSpawnInfo[] Infos
	{
		get
		{
			var _infos = new SpotSpawnInfo[SpotsCount];
			int enemyCount = InfoAll.EnemyCount / SpotsCount;
			float delay = InfoAll.Delay;
			int timer = InfoAll.Timer;
			for (int i = 0; i < _infos.Length; i++)
				_infos[i] = new SpotSpawnInfo(enemyCount, delay, timer);
			return _infos;
		}
	}
}

