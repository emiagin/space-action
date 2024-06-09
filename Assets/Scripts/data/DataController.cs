using UnityEngine;

public class DataController: MonoBehaviour
{
	[SerializeField]
	private TextAsset jsonData;

	private IDataParser parser = new DataParserFromJson();

	public WeaponParameters LoadWeaponData()
	{
		return new WeaponParameters(jsonData.text, parser);
	}
}