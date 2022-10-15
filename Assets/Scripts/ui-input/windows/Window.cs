using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
	[SerializeField]
	protected string _windowName;
	public string WindowName => _windowName;

	[SerializeField]
	protected GameObject window;
	[SerializeField]
	protected bool shouldActiveOnStart = false;

	public virtual void InitWindow()
	{
		SetWindow(shouldActiveOnStart);
	}

	public virtual void SetWindow(bool on)
	{
		window.SetActive(on);
	}
}
