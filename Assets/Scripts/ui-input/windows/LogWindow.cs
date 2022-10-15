using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LogWindow : Window
{
	[SerializeField]
	private TMP_Text logText;

	public override void InitWindow()
	{
		LogController.Instance.OnLogChange += UpdateLogText;
		base.InitWindow();
	}

	private void UpdateLogText(string newText)
	{
		logText.text = newText;
	}

	~LogWindow()
	{
		LogController.Instance.OnLogChange -= UpdateLogText;
	}
}
