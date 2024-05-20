using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum HeroLookState { Up, Down }
public class HeroBehaviour : MonoBehaviour
{
	[SerializeField]
	private Animator heroAnimator;
	[SerializeField]
	private HeroMoving heroMoving;
	[SerializeField]
	private ParametersController parameters;
	[SerializeField]
	private Weapon weapon;

	private Transform _currentPosition;
	public Transform CurrentPosition => _currentPosition;

	private Vector2 _directionView;
	public Vector2 DirectionView => _directionView;

	private HeroLookState _lookMoving;
	public HeroLookState LookMoving
	{
		get => _lookMoving;
		private set
		{
			if (value == _lookMoving)
				return;
			_lookMoving = value;
			//Debug.Log($"Debug animator hero move = {value}");
			switch (_lookMoving)
			{
				case HeroLookState.Down:
					heroAnimator.SetTrigger("move_down");
					break;
				case HeroLookState.Up:
					heroAnimator.SetTrigger("move_up");
					break;
				/*case HeroLookState.Left:
					heroAnimator.SetTrigger("move_left");
					break;
				case HeroLookState.Right:
					heroAnimator.SetTrigger("move_right");
					break;*/
			}
		}
	}

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

		heroMoving.OnHeroMoving += OnMoveAnimation;
		heroMoving.OnHeroStartMoving += SetMovingStateAnimetion;
		heroMoving.OnHeroStopMoving += SetMovingStateAnimetion;
	}

	~HeroBehaviour()
	{
		heroMoving.OnHeroMoving -= OnMoveAnimation;
		heroMoving.OnHeroStartMoving -= SetMovingStateAnimetion;
		heroMoving.OnHeroStopMoving -= SetMovingStateAnimetion;
	}

	private void Update()
	{
		_pressedLeftArrow = Input.GetKey(KeyCode.LeftArrow);
		_pressedRightArrow = Input.GetKey(KeyCode.RightArrow);
		_pressedUpArrow = Input.GetKey(KeyCode.UpArrow);
		_pressedDownArrow = Input.GetKey(KeyCode.DownArrow);

		_currentPosition = transform;

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
		_isShootStart = (_pressedLeftArrow || _pressedRightArrow || _pressedUpArrow || _pressedDownArrow) && !weapon.IsShooting;
		_isShootStop = !(_pressedLeftArrow || _pressedRightArrow || _pressedUpArrow || _pressedDownArrow) && weapon.IsShooting;

		//Debug.Log($"left = {_pressedLeftArrow} right = {_pressedRightArrow} up = {_pressedUpArrow} down = {_pressedDownArrow} || start = {_isShootStart} stop = {_isShootStop}");
	}

	private void SetDirectionView()
	{
		_viewY = 0;
		_viewX = 0;

		if (_pressedDownArrow)
			_viewY = -1;
		if (_pressedUpArrow)
			_viewY = 1;
		if (_pressedLeftArrow)
			_viewX = -1;
		if (_pressedRightArrow)
			_viewX = 1;

		_directionView = new Vector2(_viewX, _viewY);
		//Debug.Log($"DirectionView = {_directionView}");
	}

	private void OnMoveAnimation(Vector2 moveDirection)
	{
		//Debug.Log($"Debug Move state direction={moveDirection}");
		if (moveDirection.y < 0)
			LookMoving = HeroLookState.Up;
		if (moveDirection.y > 0)
			LookMoving = HeroLookState.Down;
	}

	private void SetMovingStateAnimetion(bool isMoving)
	{
		heroAnimator.SetBool("is_moving", isMoving);
	}
}
