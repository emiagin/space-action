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
	[SerializeField]
	private LayerMask weaponLayersInflicted;

	HeroInputs heroInputs;

	public void Init(HeroInputs heroInputs)
	{
		this.heroInputs = heroInputs;

		heroInputs.OnStartShooting += ShootingStart;
		heroInputs.OnShooting += Shooting;
		heroInputs.OnEndShooting += ShootingEnd;
	}

	~WeaponController()
	{
		heroInputs.OnStartShooting -= ShootingStart;
		heroInputs.OnShooting -= Shooting;
		heroInputs.OnEndShooting -= ShootingEnd;
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
}
