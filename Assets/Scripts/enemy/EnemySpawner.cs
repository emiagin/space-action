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
	[SerializeField]
	private EnemiesWave[] waves;

	private void Awake()
	{
		//StartSpawn(spawns);
	}

	private void StartSpawn(SpawnInfo[] spawns)
	{
		for(int i = 0; i < spawns.Length; i++)
		{
			//spawnPoints[spawns[i].SpawnNumber].StartSpawn(spawns[i].EnemyCount, spawns[i].Delay, targetHero, enemyPrefab);
		}
	}
}

[System.Serializable]
public struct EnemiesWave
{
	public EnemiesWavePart[] Parts;
	public int Timer; //�� ��������� � ��������� �����, ���� �� ������� ������
}

[System.Serializable]
public struct EnemiesWavePart
{
	public SpawnInfo[] Info;
	public int Timer; //�� ��������� � ��������� �����, ���� �� ������� ������
}

[System.Serializable]
public struct SpawnInfo
{
	public SpotSpawn Spawn;
	public int EnemyCount; //����� >0 = ������� ����� �������, ���������� �� �������
	public float Delay;
	public int Timer; //����� ������ = ������� ���� ������ �� �������
}
