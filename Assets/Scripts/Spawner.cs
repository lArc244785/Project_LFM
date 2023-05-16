using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public Enemy Prefab;
	public int Count;
	public float WaitTime;


	private float m_spawnTime;
	private int m_spawnCount;
	private int m_deadCount;

	private void Start()
	{
		m_spawnTime = Time.time + WaitTime;
	}

	public void Spawn()
	{
		m_spawnCount++;
		var clone = Instantiate(Prefab.gameObject, transform.position, Quaternion.identity);
		clone.SetActive(true);
		var enemy = clone.GetComponent<Enemy>();
		enemy.Init();
		enemy.Health.OnDead += EnmeyDead;
	}

	private void EnmeyDead()
	{
		m_deadCount++;
		if(m_spawnCount == m_deadCount)
		{
			AllSpawnEnemyDead();
		}
	}

	private void AllSpawnEnemyDead()
	{
		Debug.Log("AllSpawnEnemyDead");
	}

	private void Update()
	{
		if(m_spawnCount >= Count)
			return;

		var spawnTime = m_spawnTime - Time.time;
		if(spawnTime <= 0.0f)
		{
			Spawn();
			m_spawnTime = Time.time + WaitTime;
		}
	}

}
