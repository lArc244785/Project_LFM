using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField]
	private float m_speed;
	[SerializeField]
	private int m_damage;
	[SerializeField]
	private float m_lifeTime;
	private float m_deadTime;

	[SerializeField]
	private LayerMask m_targetMask;

	private PooledObject m_pooledObject;

	private Rigidbody m_rig;

	private BulletType m_bulletType;

	private void Start()
	{
		m_pooledObject = GetComponent<PooledObject>();
	}

	public void Init(BulletType type, int addDamage)
	{
		m_rig = GetComponent<Rigidbody>();
		m_bulletType = type;
		m_damage += addDamage;
		m_deadTime = Time.time + m_lifeTime;
	}

	private void Update()
	{
		m_rig.velocity = transform.forward * m_speed;
		if(Time.time >= m_deadTime)
		{
			m_pooledObject.Release();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		int mask = 1 << other.gameObject.layer;
		if ((m_targetMask & mask) == 0)
			return;

		var damagable = other.gameObject.GetComponent<Damagable>();
		if (damagable == null)
			return;

		damagable.OnDamge(m_damage);
		m_pooledObject.Release();
	}
}
