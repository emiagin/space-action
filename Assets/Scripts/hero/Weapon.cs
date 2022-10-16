using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField]
	private HeroBehaviour heroBehaviour;
	[SerializeField]
	private Collider2D[] edgeColliders;
	[SerializeField]
	private Collider2D heroCollider;
	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
	private int _damage;
	[SerializeField]
	private float _delay;
	[SerializeField]
	private float _speed;

	public int Damage => _damage;
	public float Delay => _delay;
	public float Speed => _speed;


	private bool _isShooting;
	public bool IsShooting => _isShooting;

	public void StartShoot()
	{
		_isShooting = true;
		AddNewBullet();
	}

	public void StopShoot()
	{
		_isShooting = false;
		StopCoroutine("WaitDelay");
	}

	private void AddNewBullet()
	{
		if (!_isShooting)
			return;

		var go = Instantiate(bulletPrefab, transform);
		go.transform.localPosition = Vector3.zero;

		var colliderBullet = go.GetComponentInChildren<Collider2D>();
		Physics2D.IgnoreCollision(colliderBullet, heroCollider);
		foreach(var edge in edgeColliders)
			Physics2D.IgnoreCollision(colliderBullet, edge);

		go.GetComponent<Bullet>().StartMove(heroBehaviour.DirectionView, Speed);

		StartCoroutine("WaitDelay");
	}

	IEnumerator WaitDelay()
	{
		yield return new WaitForSeconds(Delay);
		AddNewBullet();
	}
}
