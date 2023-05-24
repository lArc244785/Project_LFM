using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAuto : WeaponBase
{

	public override bool Fire(ClickType type)
	{
		if (type == ClickType.Up)
			return false;

		if (!base.Fire(type))
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
		var bullet = m_objectPoolManager.GetPooledObject(m_bullet);

		bullet.transform.position = m_firePoint.position;
		bullet.transform.rotation = m_firePoint.rotation;
		bullet.GetComponent<Bullet>().Init(m_weaponBuff.BulletType, m_additionalDamage.Damage);
		Ammo--;
	}

}
