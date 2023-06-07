using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
	Play,
	GameClear,
	GameOver,
}

public class GameManager : MonoBehaviour
{
	public static GameState State { get; private set; }

	private void Start()
	{ 
		State = GameState.Play;
		EventManager.AddListner<GameClear>(GameClear);
		EventManager.AddListner<GameOver>(GameOver);
	}


	private void GameClear(GameClear win)
	{
		Debug.Log("GameClear");
		State = GameState.GameClear;
	}

	private void GameOver(GameOver loss)
	{
		Debug.Log("GameOver");
		State = GameState.GameOver;
	}

	public void Continue()
	{
		EventManager.ResetEvent();
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}

	private void Update()
	{
		if (State != GameState.Play && Input.anyKey)
		{
			Continue();
		}
	}
}
