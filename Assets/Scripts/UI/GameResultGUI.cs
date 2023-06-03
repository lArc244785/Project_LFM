using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameResultGUI : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI m_resultText;
	private float m_startTim;

	private void Start()
	{
		m_startTim = Time.time;
		EventManager.AddListner<PlayerWin>(DrawGameWin);
		EventManager.AddListner<PlayerLoss>(DrawGameLoss);
		gameObject.SetActive(false);
	}

	private void DrawGameWin(PlayerWin win)
	{
		DrawGameResult(true);
	}

	private void DrawGameLoss(PlayerLoss loss)
	{
		DrawGameResult(false);
	}

	public void DrawGameResult(bool isClear)
	{
		gameObject.SetActive(true);
		string result = isClear ? "Win" : "Loss";
		float runTime = Time.time - m_startTim;
		gameObject.SetActive(true);
		m_resultText.text = $"Player {result}\nPlay Time : {runTime}"; 
	}
}
