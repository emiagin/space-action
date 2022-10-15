using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
	[SerializeField]
	private TMP_Text text;
	[SerializeField]
	private string _description;

	private bool _isDescent = true;
	private bool _timerGoing = false;
	private float _startTime = 0;
	private float _endTime = 0;
	private float _timer = 0;

	public Action BeforeStartTimer;
	public Action OnStopTimer;

	public void StartTimer(float startTime, bool isDescent = true, float endTime = 0)
	{
		_isDescent = isDescent;
		_startTime = startTime;
		_endTime = endTime;
		_timer = _startTime;

		BeforeStartTimer?.Invoke();
		_timerGoing = true;
	}

	private void Update()
	{
		if(_timerGoing)
		{
			if(_isDescent ? TimerDecrease() : TimerIncrease())
			{
				_timerGoing = false;
				OnStopTimer?.Invoke();
			}
		}
	}

	private bool TimerIncrease()
	{
		_timer += Time.deltaTime;
		text.text = _description + _timer.ToString("00:00");
		return _timer >= _endTime;
	}
	private bool TimerDecrease()
	{
		_timer -= Time.deltaTime;
		text.text = _description + _timer.ToString("00:00");
		return _timer <= _endTime;
	}
}
