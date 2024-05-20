using System;
using System.Collections.Generic;
using UnityEngine;

public class ParametersController : MonoBehaviour
{
	[SerializeField]
	private string _mainName;
	public string MainName
	{
		set => _mainName = value;
		get => _mainName;
	}

	[SerializeField]
	private int _maxHealth;
	public int MaxHealth => _maxHealth;

	private int _health;
	public int Health
	{
		get => _health;
		set
		{
			_health = Mathf.Clamp(value, 0, _maxHealth);
			OnChangeHealth?.Invoke(this);

			if (_health == 0)
				OnHealthNull?.Invoke();
		}
	}
	public Action OnHealthNull;
	public Action<ParametersController> OnChangeHealth;

	[SerializeField]
	private int _damage;
	public int Damage => _damage;

	[SerializeField]
	private float _speed;
	public float Speed => _speed;

	private void Awake()
	{
		_health = _maxHealth;
	}
}
