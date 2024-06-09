using System.Collections.Generic;

public interface IDataParser
{
	Dictionary<string, object> Parse(object data, IEntityDataInfo info);
}
