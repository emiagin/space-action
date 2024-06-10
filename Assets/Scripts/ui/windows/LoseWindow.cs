using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseWindow : Window
{
	[SerializeField]
	private Button _tryAgainButton;
	[SerializeField]
	private Button _exitButton;

	public override void InitWindow()
	{
		SetWindow(shouldActiveOnStart);
		_tryAgainButton.onClick.AddListener(GameManager.Instance.RestartGame);
		_exitButton.onClick.AddListener(GameManager.Instance.ExitGame);
	}

	~LoseWindow()
	{
		_tryAgainButton.onClick.RemoveAllListeners();
		_exitButton.onClick.RemoveAllListeners();
	}
}
