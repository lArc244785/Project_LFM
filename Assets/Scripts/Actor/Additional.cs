using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AdditionalHealth
{
	public int HP { get; private set; }

	public void SetHp(int hp)
	{
		HP += hp;
	}

	public float Speed { get; private set; }
}

public class AdditionalSpeed
{
	public float Speed { get; private set; }
	public void SetSpeed(float speed)
	{
		Speed = speed;
	}
}

public class AdditionalDamage
{
	public int Damage { get; private set;}
	public void SetDamge(int damge)
	{
		Damage = damge;
	}
}

public class AdditionalGun
{
	public int Bullet { get; private set; }
	public float ReloadCoolTime { get; private set; }

	public void SetBullet(int bullet)
	{
		Bullet = bullet;
	}

	public void SetReloadCoolTime(float reloadCoolTime)
	{
		ReloadCoolTime = reloadCoolTime;
	}
}

