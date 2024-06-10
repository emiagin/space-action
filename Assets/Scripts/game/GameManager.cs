using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance
	{
		private set => _instance = value;
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<GameManager>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			return _instance;
		}
	}

	/*[SerializeField]
	private ParametersController _heroParameters;*/
	[SerializeField]
	private EnemyWavesController _enemyWavesController;
	[SerializeField]
	private Level[] _levels;

	private void Awake()
	{
		/*_heroParameters.OnHealthNull += GameEnd_HeroDie;*/
	}

	private void Start()
	{
		//StartGame();
	}

	~GameManager()
	{
		/*_heroParameters.OnHealthNull -= GameEnd_HeroDie;*/
	}

	public void StartGame()
	{
		InterfaceController.Instance.SetUIStartGame(_levels[0].LevelTime, GameEnd_LevelComplete);
		_enemyWavesController.StartEnemiesSpawnerOnLevel(_levels[0]);
	}

	private void GameEnd_LevelComplete()
	{
		InterfaceController.Instance.SetPopup("win", true);
	}

	private void GameEnd_HeroDie()
	{
		InterfaceController.Instance.SetPopup("lose", true);
		Time.timeScale = 0;
	}

	public void RestartGame()
	{
		SceneManager.LoadScene("Main");
	}

	public void ExitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}
}
