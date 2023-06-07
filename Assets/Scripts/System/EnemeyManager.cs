using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyManager : MonoBehaviour
{
	private List<Enemy> m_liveEnemy = new();
	private ObjectPoolManager m_poolManager;

	[SerializeField]
	private int m_liveEnemyCount;

	private void Awake()
	{
		m_poolManager = GameObject.Find("ObjectPoolManager").GetComponent<ObjectPoolManager>();
		EventManager.AddListner<EnemyKill>(EnemyKill);
		EventManager.AddListner<EnemyAllKill>(LiveEnemyAllKill);
	}

	public Enemy SpawnEnemy(ObjectPoolKey key, Vector3 pos)
	{
		Enemy spawnEnemy = null;
		var goEnemy = m_poolManager.GetPooledObject(key);
		goEnemy.transform.position = pos;
		spawnEnemy = goEnemy.GetComponent<Enemy>();
		spawnEnemy.Init();

		m_liveEnemy.Add(spawnEnemy);
		m_liveEnemyCount = m_liveEnemy.Count;
		return spawnEnemy;
	}

	private void EnemyKill(EnemyKill killedEnemy)
	{
		m_liveEnemy.Remove(killedEnemy.enemy);
		m_liveEnemyCount = m_liveEnemy.Count;
	}

	private void LiveEnemyAllKill(EnemyAllKill allKill)
	{
		while(m_liveEnemy.Count > 0)
		{
			m_liveEnemy[0].Dead();
		}
	}

	//Test Update
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
			EventManager.Broadcast(Events.EnemyAllKill);
	}
}
