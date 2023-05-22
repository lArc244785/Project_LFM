using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectPoolKey
{
	BulletMG,
	BulletSG,
}

public class ObjectPoolManager : MonoBehaviour
{
	private Dictionary<ObjectPoolKey, ObjectPool> m_objectPool = new();

	private void Awake()
	{
		ObjectPool[] pools = transform.GetComponentsInChildren<ObjectPool>();
		foreach(var pool in pools)
		{
			pool.SetUp();
			m_objectPool.Add(pool.Type, pool);
		}
	}

	public GameObject GetPooledObject(ObjectPoolKey key)
	{
		return m_objectPool[key].GetPooledObject();
	}
}
