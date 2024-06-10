using System;

public class WeaponParameters : EntityData
{
	private int _damage;
	private float _speed;
	private float _delay;
	public WeaponParameters(object data, IDataParser dataParser) : base(data, dataParser)
	{
		_damage = Convert.ToInt32(GetParameterByName("damage"));
		_speed = Convert.ToSingle(GetParameterByName("speed"));
		_delay = Convert.ToSingle(GetParameterByName("delay"));
	}

	public int Damage => _damage;
	public float Speed => _speed;
	public float Delay => _delay;
}
