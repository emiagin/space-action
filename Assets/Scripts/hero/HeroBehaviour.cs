using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehaviour : MonoBehaviour
{
	[SerializeField]
	private ParametersController parameters;
	[SerializeField]
	private Weapon weapon;

	[HideInInspector]
	public Vector2 DirectionView;

	private void Awake()
	{
		DirectionView = Vector2.right;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			weapon.StartShoot();
		if (Input.GetKeyUp(KeyCode.Space))
			weapon.StopShoot();
	}

}
