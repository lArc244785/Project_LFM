using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ObjectPoolKey
{
	Bullet_MG,
	Bullet_SG,
	Item_Speed,
	Item_HP,
	Item_WepaonChagenSG,
	Item_FireSpeed,
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
