using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEffectBullet : MonoBehaviour, IBuff
{
	private float m_endTime;
	[SerializeField]
	private float m_duration;
	[SerializeField]
	private float m_speed;
	[SerializeField]
	private BulletType m_bulletType;

	public float EndTime { get => m_endTime; set => m_endTime = value; }
	public float Duration { get => m_duration; }

	public BuffType Type => BuffType.EffectBullet;

	public event Action OnBuff;
	public event Action OffBuff;

	private IWeaponBuff m_buff;

	private void Start()
	{
		m_buff = GameObject.Find("Player").GetComponent<WeaponManager>();
	}

	public void Enter()
	{
		Debug.Log("Enter BuffEffectBullet ");
		m_buff.BulletType = m_bulletType;
		OnBuff?.Invoke();
	}

	public void Exit(bool isSkip = false)
	{
		Debug.Log("Exit BuffEffectBullet ");
		m_buff.BulletType = BulletType.Normal;
		if (!isSkip)
			OffBuff?.Invoke();
	}
}
