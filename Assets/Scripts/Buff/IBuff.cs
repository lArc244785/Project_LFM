using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
	Speed,
	EffectBullet,
	FireSpeed,
	WeaponChange,
}

public interface IBuff 
{
	public float EndTime { get; set; }
	public float Duration { get;}
	public BuffType Type { get;}
	public void Enter();
	public void Exit(bool isSkip = false);

	public event Action OnBuff;
	public event Action OffBuff;
}
