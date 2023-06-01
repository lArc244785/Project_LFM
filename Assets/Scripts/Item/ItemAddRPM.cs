using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAddRPM : ItemBase, IBuff
{
	[SerializeField]
	private int m_addRPM;
	[SerializeField]
	private float m_duration;

	public float EndTime { get; set; }

	public float Duration => m_duration;

	public BuffType Type => BuffType.AddRPM;

	public event Action OnBuff;
	public event Action OffBuff;

	private WeaponManager m_weaponManager;
	private BuffSystem m_buffSystem;

	private void Start()
	{
		var goPlayer = GameObject.Find("Player");
		m_weaponManager = goPlayer.GetComponent<WeaponManager>();
		m_buffSystem = goPlayer.GetComponent<BuffSystem>();
	}

	public void Enter()
	{
		m_weaponManager.AddRPM = m_addRPM;
		OnBuff?.Invoke();
	}

	public void Exit(bool isSkip = false)
	{
		m_weaponManager.AddRPM = 0;

		if (!isSkip)
			OffBuff?.Invoke();
	}

	public override void Use()
	{
		m_buffSystem.AddBuff(this);
		Release();
	}
}
