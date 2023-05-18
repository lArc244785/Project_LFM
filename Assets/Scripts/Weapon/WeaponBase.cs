using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum WeaponState
{
	Firable,
	Reload,
	Empty,
}

public abstract class WeaponBase : MonoBehaviour
{
	[field:SerializeField]
	public float MaxAmmo { protected set; get; }
	private float m_ammo;
	public float Ammo
	{
		set
		{
			m_ammo = value;
			m_ammo = Mathf.Clamp(m_ammo, .0f, MaxAmmo);

			if (m_ammo <= 0.0f)
				State = WeaponState.Empty;
		}
		get
		{
			return m_ammo;
		}
	}
	public WeaponState State
	{
		protected set;
		get;
	}

	protected float m_nextFireTime;

	[SerializeField]
	private float m_reloadTime;

	private float m_reloadEndTime;

	[SerializeField]
	protected GameObject m_bullet;

	[SerializeField]
	private float m_tick;

	[SerializeField]
	protected Transform m_firePoint;

	public Action OnFire;
	public Action OnStartReload;
	public Action OnFinshReload;

	protected IWeaponBuff m_weaponBuff;
	protected AdditionalAbility m_additional;

	public virtual void Init(AdditionalAbility additional, IWeaponBuff buff)
	{
		Ammo = MaxAmmo;
		m_weaponBuff = buff;
		m_additional = additional;
		State = WeaponState.Firable;
	}

	public bool CanFire()
	{
		float fireWait = m_nextFireTime - Time.time;
		bool isFirable = State == WeaponState.Firable || (State == WeaponState.Reload && Ammo > 0.0f);

		return fireWait <= .0f && isFirable;
	}

	public virtual bool Fire(ClickType type)
	{
		OnFire?.Invoke();

		if (!CanFire())
			return false;

		//������ ��Ȳ���� �߻縦 ������ �� �߻簡 ������ ��� ĵ���ϰ� �߻��Ѵ�.
		if(State == WeaponState.Reload)
		{
			State = WeaponState.Firable;
		}

		return true;
	}

	public void Reload()
	{
		if (State == WeaponState.Reload)
			return;

		Debug.Log("Reload");
		State = WeaponState.Reload;
		m_reloadEndTime = Time.time + m_reloadTime;

		OnStartReload?.Invoke();
	}

	private void FinshReload()
	{
		var waitTime = m_reloadEndTime - Time.time;

		Debug.Log($"Reloading {waitTime}");
		if (waitTime > 0.0f)
			return;
		//Finsh Reload
		Ammo = MaxAmmo;

		State = WeaponState.Firable;
		OnFinshReload?.Invoke();
	}

	protected abstract void SpawnBullet();

	protected void UpdateNextFireTime()
	{
		m_nextFireTime = Time.time + (m_tick - (m_tick * m_weaponBuff.FireCoolRatio));
	}

	/// <summary>
	/// ���� Ŭ�������� Update�� ����ϰ� �־ �ش� �޼ҵ忡 Update���� ��ɵ��� �־��ּ���.
	/// </summary>
	protected abstract void ChildUpdate();


	private void Update()
	{
		if (State == WeaponState.Reload)
			FinshReload();

		ChildUpdate();
	}
}