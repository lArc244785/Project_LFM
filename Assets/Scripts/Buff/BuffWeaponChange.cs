using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffWeaponChange : MonoBehaviour, IBuff
{
	private float m_endTime;
	[SerializeField]
	private float m_duration;
	[SerializeField]
	private int m_weaponIndex;

	public float EndTime { get => m_endTime; set => m_endTime = value; }
	public float Duration { get => m_duration; }

	public BuffType Type => BuffType.WeaponChange;

	public event Action OnBuff;
	public event Action OffBuff;

	private WeaponManager m_manager;

	private void Start()
	{
		m_manager = GameObject.Find("Player").GetComponent<WeaponManager>();
	}


	public void Enter()
	{
		m_manager.ChangeWeapon(m_weaponIndex);
		OnBuff?.Invoke();
	}

	public void Exit(bool isSkip = false)
	{
		m_manager.ChangeWeapon(0);

		if (!isSkip)
			OffBuff?.Invoke();
	}
}
