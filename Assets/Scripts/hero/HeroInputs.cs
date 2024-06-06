using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class HeroInputs : MonoBehaviour
{
	InputControls controls;
	InputAction heroMovingAction,heroShootingAction;

	public Action<Vector2> OnStartShooting, OnShooting, OnEndShooting;
	public Action<Vector2> OnStartMoving, OnMoving, OnEndMoving;

	public void Init()
	{
		controls = new InputControls();
		heroMovingAction = controls.input.move;
		heroShootingAction = controls.input.shoot;

		heroMovingAction.started += MovingStarted;
		heroMovingAction.performed += MovingPerformed;
		heroMovingAction.canceled += MovingEnded;

		heroShootingAction.started += ShootingStarted;
		heroShootingAction.performed += ShootingPerformed;
		heroShootingAction.canceled += ShootingEnded;
	}
	
	~HeroInputs()
	{
		heroMovingAction.started -= MovingStarted;
		heroMovingAction.performed -= MovingPerformed;
		heroMovingAction.canceled -= MovingEnded;

		heroShootingAction.started -= ShootingStarted;
		heroShootingAction.performed -= ShootingPerformed;
		heroShootingAction.canceled -= ShootingEnded;
	}
	private void OnEnable()
	{
		heroMovingAction?.Enable();
		heroShootingAction?.Enable();

	}
	private void OnDisable()
	{
		heroMovingAction?.Disable();
		heroShootingAction?.Disable();
	}
	private void MovingStarted(CallbackContext callbackContext)
	{
		//Debug.Log($"moving start");
		OnStartMoving?.Invoke(heroMovingAction.ReadValue<Vector2>());
	}

	private void MovingPerformed(CallbackContext callbackContext)
	{
		//Debug.Log($"moving");
		OnMoving?.Invoke(heroMovingAction.ReadValue<Vector2>());
	}
	private void MovingEnded(CallbackContext callbackContext)
	{
		//Debug.Log($"moving end");
		OnEndMoving?.Invoke(heroMovingAction.ReadValue<Vector2>());
	}
	private void ShootingStarted(CallbackContext callbackContext)
	{
		//Debug.Log($"shooting start");
		OnStartShooting?.Invoke(heroShootingAction.ReadValue<Vector2>());
	}
	private void ShootingPerformed(CallbackContext callbackContext)
	{
		//Debug.Log($"shooting pos = {heroShootingAction.ReadValue<Vector2>()}");
		OnShooting?.Invoke(heroShootingAction.ReadValue<Vector2>());
	}

	private void ShootingEnded(CallbackContext callbackContext)
	{
		//Debug.Log($"shooting end");
		OnEndShooting?.Invoke(heroShootingAction.ReadValue<Vector2>());
	}
}
