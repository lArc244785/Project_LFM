using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState
{
	Firable,
	Reload,
	Empty,
}

public abstract class WeaponBase : MonoBehaviour
{
	[SerializeField]
	protected float mMaxAmmo;
	private float mAmmo;
	public float Ammo
	{
		set
		{
			mAmmo = value;
			mAmmo = Mathf.Clamp(mAmmo, .0f, mMaxAmmo);

			if (mAmmo <= 0.0f)
				State = WeaponState.Empty;
		}
		get
		{
			return mAmmo;
		}
	}
	public WeaponState State
	{
		protected set;
		get;
	}

	protected float mNextFireTime;

	[SerializeField]
	private float mReloadTime;

	private float mReloadEndTime;

	[SerializeField]
	protected GameObject mBullet;

	[SerializeField]
	private float mTick;

	[SerializeField]
	protected Transform mFirePoint;

	public bool CanFire()
	{
		float fireWait = mNextFireTime - Time.time;
		bool isFirable = State == WeaponState.Firable || (State == WeaponState.Reload && Ammo > 0.0f);

		return fireWait <= .0f && isFirable;
	}

	public virtual bool Fire(ClickType type)
	{
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
		mReloadEndTime = Time.time + mReloadTime;
	}

	private void ChackReload()
	{
		var waitTime = mReloadEndTime - Time.time;

		Debug.Log($"Reloading {waitTime}");
		if (waitTime > 0.0f)
			return;

		Ammo = mMaxAmmo;
		State = WeaponState.Firable;
	}

	protected abstract void SpawnBullet();

	protected void UpdateNextFireTime()
	{
		mNextFireTime = Time.time + mTick;
	}

	/// <summary>
	/// 상위 클래스에서 Update를 사용하고 있어서 해당 메소드에 Update관련 기능들을 넣어주세요.
	/// </summary>
	protected abstract void ChildUpdate();


	private void Update()
	{
		if (State == WeaponState.Reload)
			ChackReload();

		ChildUpdate();
	}
}
