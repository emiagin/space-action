using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class DataParserFromJson : IDataParser
{
	public Dictionary<string, object> Parse(object data)
	{
		var values = new Dictionary<string, object>();

		if (data is string jsonString)
		{
			var jsonObject = JObject.Parse(jsonString);

			foreach (var property in jsonObject.Properties())
			{
				values[property.Name] = property.Value.ToObject<object>();
			}
		}
		else
		{
			throw new ArgumentException("Data must be a JSON string.");
		}

		return values;
	}

	private object GetDefaultValue(Type type)
	{
		if (type.IsValueType)
		{
			return Activator.CreateInstance(type);
		}
		return null;
	}
}
