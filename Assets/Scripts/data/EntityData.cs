using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityData
{
	protected Dictionary<string, object> values;

	public EntityData(object data, IDataParser dataParser)
	{
		values = dataParser.Parse(data);
	}

	public void SetParameterByName(string name, object value)
	{
		if (!values.TryGetValue(name, out object oldValue))
			return;

		values[name] = value;
	}

	public object GetParameterByName(string name)
	{
		if (values.TryGetValue(name, out object value))
			return value;

		return null;
	}
}