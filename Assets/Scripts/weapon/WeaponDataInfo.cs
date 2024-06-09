using System.Collections.Generic;
using System;

public class WeaponDataInfo : IEntityDataInfo
{
	static Dictionary<string, Type> _info = new Dictionary<string, Type>()
	{
		{ "damage", typeof(int) },
		{ "delay", typeof(float) },
		{ "speed", typeof(float) }
	};

	public Dictionary<string, Type> Info => _info;
}