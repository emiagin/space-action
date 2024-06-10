using UnityEngine;

public class DataController: MonoBehaviour
{
	[SerializeField]
	private TextAsset weaponData;
	[SerializeField]
	private TextAsset enemyData;

	private IDataParser parser = new DataParserFromJson();

	public WeaponParameters LoadWeaponData()
	{
		return new WeaponParameters(weaponData.text, parser);
	}

	public EnemyParameters LoadEnemyData()
	{
		return new EnemyParameters(enemyData.text, parser);
	}
}