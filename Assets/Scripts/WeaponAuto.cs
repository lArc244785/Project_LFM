using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAuto : WeaponBase
{
	private void Start()
	{
		Ammo = mMaxAmmo;
	}

	public override bool Fire(ClickType type)
	{
		if (!base.Fire(type))
			return false;

		if (type == ClickType.Up)
			return false;

		SpawnBullet();
		UpdateNextFireTime();

		return true;
	}

	protected override void ChildUpdate()
	{
		
	}

	protected override void SpawnBullet()
	{
		var bullet = Instantiate(mBullet);
		bullet.transform.position = mFirePoint.position;
		bullet.transform.rotation = mFirePoint.rotation;
		bullet.SetActive(true);
		Ammo--;
		Debug.Log($"SpawnBullet {Ammo}");
	}

}
