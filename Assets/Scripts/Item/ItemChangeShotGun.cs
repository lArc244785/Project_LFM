using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChangeShotGun : ItemBase, IBuff
{
	public float EndTime { get; set; }

	[SerializeField]
	private float m_duration;
	public float Duration => m_duration;

	public BuffType Type => BuffType.WeaponChange;

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
		m_weaponManager.SetWeapon(WeaponIndex.ShotGun);
	}

	public void Exit(bool isSkip = false)
	{
		m_weaponManager.SetWeapon(WeaponIndex.MachineGun);
	}

	public override void Use()
	{
		m_buffSystem.AddBuff(this);
		Release();
	}

}
