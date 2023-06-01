using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemSpeedUp : ItemBase, IBuff
{
	[SerializeField]
	private float m_speedUp;
	[SerializeField]
	private float m_duration;

	public float Duration => m_duration;

	public BuffType Type => BuffType.Speed;

	public float EndTime { get; set; }

	public event Action OnBuff;
	public event Action OffBuff;

	private PlayerMovement m_movement;
	private BuffSystem m_buffSystem;

	private void Start()
	{
		var goPlayer = GameObject.Find("Player");
		m_movement = goPlayer.GetComponent<PlayerMovement>();
		m_buffSystem = goPlayer.GetComponent<BuffSystem>();
	}

	public void Enter()
	{
		m_movement.Speed += m_speedUp;
		OnBuff?.Invoke();
	}

	public void Exit(bool isSkip = false)
	{
		m_movement.Speed -= m_speedUp;
		OffBuff?.Invoke();
	}

	public override void Use()
	{
		m_buffSystem.AddBuff(this);
		Release();
	}
}
