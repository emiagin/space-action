using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WeaponController: MonoBehaviour
{
	[SerializeField]
	private WeaponBehavior weaponBehavior;
	//[SerializeField]
	//private LayerMask weaponLayersInflicted;

	private HeroInputs heroInputs;
	private WeaponParameters weaponData;

	public Action<EnemyController> onEnemyDamaged;

	public void Init(HeroInputs heroInputs, WeaponParameters weaponData)
	{
		this.heroInputs = heroInputs;
		this.weaponData = weaponData;

		weaponBehavior.Init(weaponData);

		heroInputs.OnStartShooting += ShootingStart;
		heroInputs.OnShooting += Shooting;
		heroInputs.OnEndShooting += ShootingEnd;

		onEnemyDamaged += EnemyDamaged;
	}

	~WeaponController()
	{
		heroInputs.OnStartShooting -= ShootingStart;
		heroInputs.OnShooting -= Shooting;
		heroInputs.OnEndShooting -= ShootingEnd;

		onEnemyDamaged -= EnemyDamaged;
	}

	private void ShootingStart(Vector2 direction)
	{
		weaponBehavior.SetDirection(direction);
		weaponBehavior.StartShooting();
	}

	private void Shooting(Vector2 direction)
	{
		weaponBehavior.SetDirection(direction);
	}
	private void ShootingEnd(Vector2 direction)
	{
		weaponBehavior.StopShooting();
	}
	private void EnemyDamaged(EnemyController enemy)
	{
		enemy.TakeDamage(weaponData.Damage); 
	}
}
