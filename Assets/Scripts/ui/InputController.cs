using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	private static InputController _instance;
	public static InputController Instance
	{
		private set => _instance = value;
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<InputController>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}


}
