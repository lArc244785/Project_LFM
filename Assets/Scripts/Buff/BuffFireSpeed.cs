using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffFireSpeed : MonoBehaviour, IBuff
{
	private float m_endTime;
	[SerializeField]
	private float m_duration;
	[SerializeField, Range(0.0f, 1.0f)]
	private float m_fireSpeedRatio;

	public float EndTime { get => m_endTime; set => m_endTime = value; }
	public float Duration { get => m_duration; }

	public BuffType Type => BuffType.FireSpeed;

	public event Action OnBuff;
	public event Action OffBuff;

	private IWeaponBuff m_buff;

	// Start is called before the first frame update
	void Start()
    {
		m_buff = GameObject.Find("Player").GetComponent<WeaponManager>();
	}

	public void Enter()
	{
		Debug.Log("Enter BuffFireSpeed ");
		m_buff.FireCoolRatio = m_fireSpeedRatio;
		OnBuff?.Invoke();
	}

	public void Exit(bool isSkip = false)
	{
		Debug.Log("Exit BuffFireSpeed ");
		m_buff.FireCoolRatio = 0.0f;

		if (!isSkip)
			OffBuff?.Invoke();
	}

}
