using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
	[SerializeField]
	private GameObject bulletPrefab;

	private WeaponParameters weaponParameters;
	private WeaponController weaponController;
	private Collider2D heroCollider;

	private bool _isShooting;
	public bool IsShooting => _isShooting;

	private Vector2 direction;

	public void Init(WeaponParameters weaponParameters, WeaponController weaponController, Collider2D heroCollider)
	{
		this.weaponParameters = weaponParameters;
		this.weaponController = weaponController;
		this.heroCollider = heroCollider;
	}

	public void StartShooting()
	{
		_isShooting = true;
		AddNewBullet();
	}

	public void StopShooting()
	{
		_isShooting = false;
		StopCoroutine("WaitDelay");
	}

	public void SetDirection(Vector2 newDirection)
	{
		direction = newDirection;
	}

	private void AddNewBullet()
	{
		if (!_isShooting)
			return;

		var go = Instantiate(bulletPrefab, transform);
		go.transform.localPosition = Vector3.zero;

		var colliderBullet = go.GetComponentInChildren<Collider2D>();
		Physics2D.IgnoreCollision(colliderBullet, heroCollider);

		go.GetComponent<Bullet>().Init(weaponController, direction, weaponParameters.Speed);

		StartCoroutine("WaitDelay");
	}

	IEnumerator WaitDelay()
	{
		yield return new WaitForSeconds(weaponParameters.Delay);
		AddNewBullet();
	}
}
