using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Health Health { private set; get; }


	public void Init()
	{
		Health = GetComponent<Health>();
		Health.Init(10);
		Health.OnDead += Dead;
	}

	private void Dead()
	{
		Health.OnDead -= Dead;
		Destroy(gameObject);
	}
}
