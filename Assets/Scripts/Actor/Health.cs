using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	private int m_maxHp;
	public int MaxHp => m_maxHp + AdditionalHealth.HP;
	public int HP { get; private set; }
	public bool IsDead { get; private set; }
	public bool IsGodMode { get; set; }

	public event Action OnHeal;
	public event Action OnHit;
	public event Action OnDead;

	public AdditionalHealth AdditionalHealth { get; private set; } = new();

	public void Init(int maxHp)
	{
		m_maxHp = maxHp;
		HP = MaxHp;
	}

	public void TakeDamage(int damage)
	{
		if (IsGodMode)
			return;

		HP -= damage;
		Debug.Log($"Take Damage{gameObject.name} {HP}");
		if (HP <= 0)
		{
			Dead();
			return;
		}

		OnHit?.Invoke();
	}

	public void Heal(int heal)
	{
		HP += heal;
		HP = Mathf.Clamp(HP, 0, MaxHp);
		OnHeal?.Invoke();
	}

	private void Dead()
	{
		Debug.Log($"{gameObject.name} is Dead!!!");
		OnDead?.Invoke();
	}

	


}
