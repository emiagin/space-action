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
	private WeaponController equippedWeapon;
	[SerializeField]
	private DataController dataController;

	private Collider2D heroCollider;
	public Transform CurrentPosition => transform;

	private void Awake()
	{
		heroCollider = GetComponentInChildren<Collider2D>();

		heroInputs.Init();
		heroMoving.Init(heroInputs);

		equippedWeapon.Init(heroInputs, dataController.LoadWeaponData(), heroCollider);
	}
}
