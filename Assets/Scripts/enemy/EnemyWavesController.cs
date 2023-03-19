using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesController : MonoBehaviour
{
	[SerializeField]
	private EnemySpawner _enemySpawner;

	private Level _level;
	private int _currentWaveIndex;

	public void StartEnemiesSpawnerOnLevel(Level level)
	{
		_level = level;
		_currentWaveIndex = -1;
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

		_enemySpawner.StartSpawn(_level.EnemiesWaves[_currentWaveIndex].Infos);
		StartCoroutine("WaitWaveTime");
	}

	private void StopWaves()
	{

	}

	IEnumerator WaitWaveTime()
	{
		yield return new WaitForSeconds(_level.LevelTime);
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

