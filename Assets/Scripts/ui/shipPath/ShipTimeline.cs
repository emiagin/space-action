using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipTimeline : MonoBehaviour
{
	[SerializeField]
	private RectTransform finishPoint;
	[SerializeField]
	private RectTransform ship;

	private float _speedShip;
	private bool _isMoving;
	private Vector2 _dir = new Vector2(0, 1f);

	private void FixedUpdate()
	{
		if (_isMoving)
		{
			//Debug.Log($"Delta ship/final = {Vector3.Distance(finishPoint.position, ship.position)}");
			ship.position = ship.position + new Vector3(_dir.x * _speedShip, _dir.y * _speedShip, 0);
			if (Vector3.Distance(finishPoint.position, ship.position) < 1f)
				StopMove();
		}
	}

	public void StartMove(int levelTime)
	{
		var distance = Vector3.Distance(finishPoint.position, ship.position);
		var _speedPerSec = distance / levelTime;
		_speedShip = _speedPerSec * Time.deltaTime;
		_isMoving = true;
	}

	public void StopMove()
	{
		_isMoving = false;
	}
}
