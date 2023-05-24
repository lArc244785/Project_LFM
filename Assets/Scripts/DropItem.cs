using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
	public List<ObjectPoolKey> ItemList;
	private ObjectPoolManager poolManager;
	private Health m_health;
	[SerializeField,Range(0.0f, 1.0f)]
	private float m_ratio;

	private void Start()
	{
		poolManager = GameObject.Find("ObjectPoolManager").GetComponent<ObjectPoolManager>();
		m_health = GetComponent<Health>();
		m_health.OnDead += Drop;
	}

	public void Drop()
	{
		float dropRatio = Random.Range(0.0f, 1.0f);
		if (dropRatio > m_ratio)
			return;

		int random = Random.Range(0, ItemList.Count - 1);
		var randomItem = ItemList[random];
		var dropItem = poolManager.GetPooledObject(randomItem);
		Vector3 spawnPoint = transform.position;
		spawnPoint.y = 0.5f;
		dropItem.transform.position = spawnPoint;
	}

	private void OnDestroy()
	{
		if(m_health != null)
		m_health.OnDead -= Drop;
	}
}
