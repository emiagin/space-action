using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
	[SerializeField]
	private HpBarPoint pointPrefab;

	private HpBarPoint[] _allHpPoints;

	private void Awake()
	{
		if (transform.childCount != 0)
		{
			foreach (Transform child in transform)
				GameObject.Destroy(child.gameObject);
		}	
	}

	public void InitHpBar(int healthCount)
	{
		_allHpPoints = new HpBarPoint[healthCount];
		for (int i = 0; i < healthCount; i++)
		{
			var go = Instantiate(pointPrefab, transform);
			go.transform.localPosition = Vector3.zero;

			_allHpPoints[i] = go.GetComponent<HpBarPoint>();
		}
	}
	/*
	public void ChangeHpBar(ParametersController parameters)
	{
		var hpDelta = parameters.MaxHealth - parameters.Health;
		for(int i = 0; i < hpDelta; i++)
			_allHpPoints[i].SetPoint(false);
	}*/
}
