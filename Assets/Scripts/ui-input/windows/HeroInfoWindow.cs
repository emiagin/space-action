using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInfoWindow : Window
{
	[SerializeField]
	private HpBar hpBar;

	/*private ParametersController _heroParams;*/

	public override void InitWindow()
	{
		/*_heroParams = GameObject.FindGameObjectWithTag("Player").GetComponent<ParametersController>();
		hpBar.InitHpBar(_heroParams.MaxHealth);
		_heroParams.OnChangeHealth += hpBar.ChangeHpBar;*/

		base.InitWindow();
	}

	~HeroInfoWindow()
	{
		/*_heroParams.OnChangeHealth -= hpBar.ChangeHpBar;*/
	}
}
