using System;
using UnityEngine;

public class ShipInfoWindow : Window
{
	[SerializeField]
	private Timer timer;
	[SerializeField]
	private ShipTimeline shipTimeline;

	public void StartShipWindow(int time, Action actionStopTimer = null)
	{
		shipTimeline.StartMove(time);
		if(actionStopTimer != null)
			timer.OnStopTimer += actionStopTimer;
		timer.StartTimer(time);
	}
}
