using UnityEngine;

public class DataController: MonoBehaviour
{
	[SerializeField]
	private TextAsset weaponData;
	[SerializeField]
	private TextAsset enemyData;
	[SerializeField]
	private TextAsset heroData;

	private IDataParser parser = new DataParserFromJson();

	public WeaponParameters LoadWeaponData()
	{
		return new WeaponParameters(weaponData.text, parser);
	}

	public EnemyParameters LoadEnemyData()
	{
		return new EnemyParameters(enemyData.text, parser);
	}
	public HeroParameters LoadHeroData()
	{
		return new HeroParameters(heroData.text, parser);
	}
}