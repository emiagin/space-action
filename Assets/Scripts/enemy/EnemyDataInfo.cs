using System.Collections.Generic;
using System;

public class EnemyDataInfo : IEntityDataInfo
{
	static Dictionary<string, Type> _info = new Dictionary<string, Type>()
	{
		{ "health", typeof(int) }
	};
	public Dictionary<string, Type> Info => _info;
}