using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField]
	private Collider2D collider;

	private float _speed;
	private Vector2 _dir;
	private bool _isMoving = false;

	private void FixedUpdate()
	{
		if (_isMoving)
		{
			//Debug.Log($"Bullet pos = {transform.position}");
			transform.position = transform.position + new Vector3(_dir.x * _speed, _dir.y * _speed, 0);
		}
	}
	public void StartMove(Vector2 dir, float speed)
	{
		_speed = speed;
		_dir = dir;
		_isMoving = true;
	}
}
