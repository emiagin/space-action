﻿using System;

public class EnemyParameters : EntityData
{
	private int _health;
	private float _speed;
	private int _damage;
	private int _currentHealth;

	public EnemyParameters(object data, IDataParser dataParser) : base(data, dataParser)
	{
		_health = Convert.ToInt32(GetParameterByName("health"));
		_speed = Convert.ToSingle(GetParameterByName("speed"));
		_damage = Convert.ToInt32(GetParameterByName("damage"));

		_currentHealth = Health;
	}
	public int Health => _health;
	public float Speed => _speed;
	public int Damage => _damage;
	public int CurrentHealth 
	{
		set {
			if(value < 0)
				value = 0;
			_currentHealth = value;
		}
		get => _currentHealth;
	}
}
