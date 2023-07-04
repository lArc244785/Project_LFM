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

	private ObjectPoolManager m_poolManager;
	private PooledObject m_pooledObject;
	private NavMeshMovement m_movement;

	private IEnumerator m_onSpeedDown;

	private void Awake()
	{
		m_target = LayerMask.GetMask("Player");
		m_pooledObject = GetComponent<PooledObject>();
		Health = GetComponent<Health>();
		m_movement = GetComponent<NavMeshMovement>();
		m_poolManager = GameObject.Find("ObjectPoolManager").GetComponent<ObjectPoolManager>();

		Health.OnDead += Dead;
		Health.OnHit += SpeedDown;
	}

	public void Init()
	{
		Health.Init();
	}

	public void Dead()
	{
		Events.EnemyKill.enemy = this;

		var deadEffect =m_poolManager.GetPooledObject(ObjectPoolKey.Effect_ActorDead);
		deadEffect.transform.position = transform.position;
		deadEffect.GetComponent<EffectAutoRelease>().Play();

		EventManager.Broadcast(Events.EnemyKill);
		m_pooledObject.Release();
	}

	private void SpeedDown()
	{
		if(m_onSpeedDown != null)
			StopCoroutine(m_onSpeedDown);

		m_onSpeedDown = OnSpeedDown();
		StartCoroutine(m_onSpeedDown);
	}

	private IEnumerator OnSpeedDown()
	{
		m_movement.Speed = m_movement.Speed * 0.5f;
		yield return new WaitForSeconds(0.2f);
		m_movement.Speed = m_movement.MaxSpeed;
		m_onSpeedDown = null;
	}

	private void OnTriggerEnter(Collider other)
	{
		int player = 1 << other.gameObject.layer;

		if ((m_target & player) == 0)
			return;

		other.gameObject.GetComponent<Health>().TakeDamage(m_damage);
	}
}
