using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHP : MonoBehaviour, IItem
{
	[SerializeField]
	private int m_hp;
	private Health m_health;

	private PooledObject m_pooledObject;
	public ObjectPoolKey Key => m_pooledObject.Pool.Type;

	private void Start()
	{
		m_health = GameObject.Find("Player").GetComponent<Health>();
		m_pooledObject = GetComponent<PooledObject>();
	}

	public void Use()
	{
		m_health.Heal(m_hp);
		m_pooledObject.Release();
	}
}
