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
	[field: SerializeField]
	private int m_maxAmmo;
	public int MaxAmmo => m_maxAmmo + m_additionalGun.Bullet;
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
	protected ObjectPoolKey m_bullet;
	protected ObjectPoolManager m_objectPoolManager;

	[SerializeField]
	private float m_tick;

	[SerializeField]
	protected Transform m_firePoint;

	public Action OnFire;
	public Action OnStartReload;
	public Action OnFinshReload;

	protected IWeaponBuff m_weaponBuff;

	protected AdditionalGun m_additionalGun;
	protected AdditionalDamage m_additionalDamage;

	private void Start()
	{
		m_objectPoolManager = GameObject.Find("ObjectPoolManager").GetComponent<ObjectPoolManager>();
	}

	public virtual void Init(AdditionalGun addGun, AdditionalDamage addDamage, IWeaponBuff buff)
	{
		m_additionalGun = addGun;
		m_additionalDamage = addDamage;
		Ammo = MaxAmmo;
		m_weaponBuff = buff;
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

		//재장전 상황에서 발사를 눌렀을 때 발사가 가능한 경우 캔슬하고 발사한다.
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
		m_reloadEndTime = Time.time + m_reloadTime - (m_reloadTime * m_additionalGun.ReloadCoolTime);

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
	/// 상위 클래스에서 Update를 사용하고 있어서 해당 메소드에 Update관련 기능들을 넣어주세요.
	/// </summary>
	protected abstract void ChildUpdate();


	private void Update()
	{
		if (State == WeaponState.Reload)
			FinshReload();

		ChildUpdate();
	}
}
