using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSingle : WeaponBase
{
	private void Start()
	{
		Ammo = MaxAmmo;
	}
	public override bool Fire(ClickType type)
	{
		if (!base.Fire(type))
			return false;

		if (type != ClickType.Down)
			return false;

		Ammo--;
		SpawnBullet();
		UpdateNextFireTime();

		return true;
	}

	protected override void ChildUpdate()
	{
		
	}

	protected override void SpawnBullet()
	{
		Debug.Log("SpawnBullet");
	}

}
