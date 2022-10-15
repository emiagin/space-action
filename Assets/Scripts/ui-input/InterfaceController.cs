using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
	private static InterfaceController _instance;
	public static InterfaceController Instance
	{
		private set => _instance = value;
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<InterfaceController>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}

	[SerializeField]
	private ParametersController _heroParameters;
	[SerializeField]
	private Dictionary<string, Window> _popups;

	private void Awake()
	{
		_popups = new Dictionary<string, Window>();
		var allPopups = FindObjectsOfType<Window>();
		foreach (var pop in allPopups)
		{
			_popups.Add(pop.WindowName, pop);
			pop.InitWindow();
		}
	}

	public void SetPopup(string name, bool on)
	{
		if (_popups.ContainsKey(name))
		{
			_popups[name].SetWindow(on);
		}
		else
			Debug.LogError($"There is no popup with name {name}");
	}

	public void SetUIStartGame(int levelTime, Action actionStopTimer = null)
	{
		(_popups["ship_info"] as ShipInfoWindow).StartShipWindow(levelTime, actionStopTimer);
	}
}
