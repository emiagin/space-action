using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollectorController : MonoBehaviour
{
	private static CollectorController _instance;
	public static CollectorController Instance
	{
		private set => _instance = value;
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<CollectorController>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}

	[SerializeField]
	private Bar _collectorBar;

	private int _currentEnergy = 0;
	private int _allEnergy = 10;

	private void Awake()
	{
		_collectorBar.ChangeFill(_currentEnergy / (float)_allEnergy);
	}

	public void AddKill(int energy)
	{
		_currentEnergy += energy;
		_collectorBar.ChangeFill(_currentEnergy / (float)_allEnergy);
	}
}
