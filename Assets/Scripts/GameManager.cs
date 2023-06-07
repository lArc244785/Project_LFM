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
		EventManager.AddListner<PlayerWin>(GameClear);
		EventManager.AddListner<PlayerLoss>(GameOver);
	}


	private void GameClear(PlayerWin win)
	{
		Debug.Log("GameClear");
		State = GameState.GameClear;
	}

	private void GameOver(PlayerLoss loss)
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
