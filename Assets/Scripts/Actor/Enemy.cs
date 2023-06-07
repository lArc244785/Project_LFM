using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PooledObject))]
public class Enemy : MonoBehaviour
{
	public Health Health { private set; get; }

	private LayerMask m_target;
	[SerializeField]
	private int m_damage;

	private PooledObject m_pooledObject;


	private void Awake()
	{
		m_target = LayerMask.GetMask("Player");
		m_pooledObject = GetComponent<PooledObject>();
		Health = GetComponent<Health>();
		Health.OnDead += Dead;
	}

	public void Init()
	{
		Health.Init();
	}

	public void Dead()
	{
		Events.EnemyKill.enemy = this;
		EventManager.Broadcast(Events.EnemyKill);
		m_pooledObject.Release();
	}

	private void OnTriggerEnter(Collider other)
	{
		int player = 1 << other.gameObject.layer;

		if ((m_target & player) == 0)
			return;

		other.gameObject.GetComponent<Health>().TakeDamage(m_damage);
	}
}
