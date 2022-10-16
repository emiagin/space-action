using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBehaviour : MonoBehaviour
{
	[SerializeField]
	private ParametersController parameters;
	[SerializeField]
	private Weapon weapon;

	private Vector2 _directionView;
	public Vector2 DirectionView => _directionView;

	private float _viewX, _viewY;

	private bool _pressedLeftArrow, _pressedRightArrow, _pressedUpArrow, _pressedDownArrow;
	private bool _isShootStart, _isShootStop;

	private void Awake()
	{
		_pressedLeftArrow = false;
		_pressedRightArrow = false;
		_pressedUpArrow = false;
		_pressedDownArrow = false;

		_isShootStart = false;
		_isShootStop = false;
	}

	private void Update()
	{
		SetDirectionView();
		SetShooting();

		if (_isShootStart)
		{
			weapon.StartShoot();
			_isShootStart = false;
		}
		if (_isShootStop)
		{
			weapon.StopShoot();
			_isShootStop = false;
		}
	}

	private void SetShooting()
	{
		_pressedLeftArrow = Input.GetKey(KeyCode.LeftArrow);
		_pressedRightArrow = Input.GetKey(KeyCode.RightArrow);
		_pressedUpArrow = Input.GetKey(KeyCode.UpArrow);
		_pressedDownArrow = Input.GetKey(KeyCode.DownArrow);

		_isShootStart = (_pressedLeftArrow || _pressedRightArrow || _pressedUpArrow || _pressedDownArrow) && !weapon.IsShooting;
		_isShootStop = !(_pressedLeftArrow || _pressedRightArrow || _pressedUpArrow || _pressedDownArrow) && weapon.IsShooting;

		//Debug.Log($"left = {_pressedLeftArrow} right = {_pressedRightArrow} up = {_pressedUpArrow} down = {_pressedDownArrow} || start = {_isShootStart} stop = {_isShootStop}");
	}

	private void SetDirectionView()
	{
		if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
			_viewY = 0;
		if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
			_viewX = 0;

		if (Input.GetKey(KeyCode.DownArrow))
			_viewY = -1;
		if (Input.GetKey(KeyCode.UpArrow))
			_viewY = 1;
		if (Input.GetKey(KeyCode.LeftArrow))
			_viewX = -1;
		if (Input.GetKey(KeyCode.RightArrow))
			_viewX = 1;

		_directionView = new Vector2(_viewX, _viewY);
		//Debug.Log($"DirectionView = {_directionView}");
	}
}
