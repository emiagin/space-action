using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityData<T> where T : IEntityDataInfo, new()
{
	protected T info = new T();
	protected Dictionary<string, object> values;

	public EntityData(object data, IDataParser dataParser)
	{
		values = dataParser.Parse(data, info);
	}

	public void SetParameterByName(string name, object value)
	{
		if (!values.TryGetValue(name, out object oldValue))
			return;

		if (info.Info[name].IsInstanceOfType(value))
		{
			values[name] = value;
		}
		else
		{
			Debug.LogError($"Invalid type for {name}. Expected {info.Info[name]} but got {value.GetType()}.");
		}
	}

	public KeyValuePair<object,Type> GetParameterByName(string name)
	{
		var result = new KeyValuePair<object,Type>();

		if (values.TryGetValue(name, out object value))
			return new KeyValuePair<object, Type>(value, info.Info[name]);

		return result;
	}
}