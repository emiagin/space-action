using System;
using System.Collections.Generic;
using UnityEngine;

public class LogController : MonoBehaviour
{
	private static LogController _instance;
	public static LogController Instance
	{
		private set => _instance = value;
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<LogController>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}

	[SerializeField]
	private bool useLog = false;

	private string _log = "";

	public Action<string> OnLogChange;

	private void Start()
	{
		if(useLog)
			Log_Init();
	}

	public void Log_Init()
	{
		_log = "<b>Game Log:</b>\n\n";
		OnLogChange?.Invoke(_log);
	}

	/*public void Log_OnEnemyAppearance(ParametersController enemy)
	{
		if (!useLog)
			return;

		_log += $"enemy <color=yellow>{enemy.MainName}</yellow> is <color=red>APPEARED</yellow>\n";
		OnLogChange?.Invoke(_log);
	}

	public void Log_OnEnemyHealthChange(ParametersController enemy)
	{
		if (!useLog)
			return;

		_log += $"enemy <color=yellow>{enemy.MainName}</color> current health = <color=yellow>{enemy.Health}</color>\n";
		if(enemy.Health == 0)
			_log += $"enemy <color=red>DEAD</color>\n";
		OnLogChange?.Invoke(_log);
	}*/
}
