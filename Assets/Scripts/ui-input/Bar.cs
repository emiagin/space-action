using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
	[SerializeField]
	private Image fillImage;

	public void ChangeFill(float fill)
	{
		fillImage.fillAmount = fill;
	}
}
