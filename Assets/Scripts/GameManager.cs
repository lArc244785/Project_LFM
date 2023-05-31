using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
	Play,
	GameOver,
}

public class GameManager : MonoBehaviour
{
	public GameState State { get; private set; }

	private GameObject GUIGameOver;
	private PlayerInputHandler m_input;

	private void Start()
	{
		State = GameState.Play;

		GUIGameOver = GameObject.Find("GameOver");
		GUIGameOver.SetActive(false);

		var goPlayer = GameObject.Find("Player");
		m_input = goPlayer.GetComponent<PlayerInputHandler>();
		goPlayer.GetComponent<Health>().OnDead += GameOver;
	}

	private void GameOver()
	{
		GUIGameOver.SetActive(true);
		State = GameState.GameOver;
	}

	public void Continue()
	{
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}
}
