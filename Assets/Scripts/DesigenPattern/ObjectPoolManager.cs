using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ObjectPoolKey
{
	Bullet_MG,
	Item_HP,
	Item_Buff_Speed,
	Item_Buff_AddRPM,
	Item_Buff_ChnageShotGun,
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
		if(!m_objectPool.ContainsKey(key))
		{
			Debug.LogError($"Not ContainsKey {key}");
			return null;
		}
		return m_objectPool[key].GetPooledObject();
	}
}
