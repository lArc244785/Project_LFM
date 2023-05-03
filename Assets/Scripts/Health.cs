using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int HP { get; private set; }
	public bool IsDead { get; private set; }
	public bool IsGodMode { get; set; }

	public event Action OnHit;
	public event Action OnDead;

	public void Init(int hp)
	{
		HP = hp;
	}

	public void TakeDamage(int damage)
	{
		if (IsGodMode)
			return;

		HP -= damage;
		Debug.Log($"Take Damage {HP}");
		if (HP <= 0)
		{
			Dead();
			return;
		}

		OnHit?.Invoke();
	}

	private void Dead()
	{
		OnDead?.Invoke();
	}

	


}
