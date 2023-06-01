using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField]
	private int m_damage;
	[SerializeField]
	private float m_lifeTime;
	[SerializeField]
	private float m_speed;

	private Vector3 m_dir;
	private float m_deadTime;

	private PooledObject m_pooledObject;

	private LayerMask m_hitMask;

	private void Start()
	{
		m_pooledObject = GetComponent<PooledObject>();
		m_hitMask = LayerMask.GetMask("Enemy");
	}

	public void Init(Transform pivot, Vector3 dir)
	{
		transform.position = pivot.position;
		transform.rotation = pivot.rotation;

		m_deadTime = Time.time + m_lifeTime;
		SetDir(dir);
	}

	private void SetDir(Vector3 dir)
	{
		m_dir = dir;
	}

	private void Update()
	{
		if(m_deadTime < Time.time)
		{
			m_pooledObject.Release();
		}

		transform.Translate(m_dir * m_speed * Time.deltaTime, Space.World);
	}

	private void OnTriggerEnter(Collider other)
	{
		int targetMask = 1 << other.gameObject.layer;

		if ((m_hitMask & targetMask) == 0)
			return;

		other.GetComponent<Damagable>().OnDamge(m_damage);
	}

}
