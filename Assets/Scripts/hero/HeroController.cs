using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HeroController: MonoBehaviour
{
	[SerializeField]
	private HeroInputs heroInputs;
	[SerializeField]
	private HeroMoving heroMoving;
	[SerializeField]
	private HpView hpView;
	[SerializeField]
	private WeaponController equippedWeapon;
	[SerializeField]
	private DataController dataController;

	private Collider2D heroCollider;
	private HeroParameters heroParameters;
	public Transform CurrentPosition => transform;

	public Action<HeroController> onHeroDamaged;
	public Action<HeroController> onHeroDestroyed;

	private void Awake()
	{
		heroCollider = GetComponentInChildren<Collider2D>();
		heroParameters = dataController.LoadHeroData();

		heroInputs.Init();
		heroMoving.Init(heroInputs);
		hpView.Init(heroParameters.CurrentHealth);

		equippedWeapon.Init(heroInputs, dataController.LoadWeaponData(), heroCollider);
	}
	public void TakeDamage(int damage)
	{
		heroParameters.CurrentHealth -= damage;
		hpView.SetHealthSprite(heroParameters.CurrentHealth);
		onHeroDamaged?.Invoke(this);

		if (heroParameters.CurrentHealth == 0)
		{
			onHeroDestroyed?.Invoke(this);
			Destroy(gameObject);
		}
	}
}
