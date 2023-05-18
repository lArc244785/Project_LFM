using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSpeed : MonoBehaviour, IBuff
{
	private float m_endTime;
	[SerializeField]
	private float m_duration;
	[SerializeField]
	private float m_speed;

	public float EndTime { get => m_endTime; set => m_endTime = value; }
	public float Duration { get => m_duration;}

	public BuffType Type => BuffType.Speed;

	private IMovable m_movable;

	public event Action OnBuff;
	public event Action OffBuff;

	private void Start()
	{
		m_movable = GameObject.Find("Player").GetComponent<IMovable>();
	}

	public void Enter()
	{
		Debug.Log("Enter BuffSpeed ");
		m_movable.Speed += m_speed;
		OnBuff?.Invoke();
	}

	public void Exit(bool isSkip = false)
	{
		Debug.Log("Exit BuffSpeed ");
		m_movable.Speed -= m_speed;

		if(!isSkip)
			OffBuff?.Invoke();
	}
}
