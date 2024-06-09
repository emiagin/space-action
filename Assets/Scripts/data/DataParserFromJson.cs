using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

public class DataParserFromJson : IDataParser
{
	public Dictionary<string, object> Parse(object data, IEntityDataInfo info)
	{
		var values = new Dictionary<string, object>();

		if (data is string jsonString)
		{
			var jsonObject = JObject.Parse(jsonString);

			foreach (var key in info.Info.Keys)
			{
				if (jsonObject.TryGetValue(key, StringComparison.OrdinalIgnoreCase, out JToken token))
				{
					var value = token.ToObject(info.Info[key]);
					values[key] = value;
				}
				else
				{
					values[key] = GetDefaultValue(info.Info[key]);
				}
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
