public class WeaponParameters : EntityData<WeaponDataInfo>
{
	public WeaponParameters(object data, IDataParser dataParser) : base(data, dataParser)
	{
	}

	public int Damage => (int)GetParameterByName("damage").Key;
	public float Speed => (float)GetParameterByName("speed").Key;
	public float Delay => (float)GetParameterByName("delay").Key;
}
