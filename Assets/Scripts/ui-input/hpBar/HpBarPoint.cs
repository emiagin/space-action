using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarPoint : MonoBehaviour
{
	[SerializeField]
	private Image fillImage;

	public bool IsFull => fillImage.isActiveAndEnabled;

	public void SetPoint(bool on)
	{
		fillImage.gameObject.SetActive(on);
	}
}
