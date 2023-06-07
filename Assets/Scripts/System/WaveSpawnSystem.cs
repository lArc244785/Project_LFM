using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnSystem : MonoBehaviour
{
	[SerializeField]
	private bool m_isDebug;
	[SerializeField]
	private float m_spawnRange;
	[SerializeField]
	private float m_deadRange;
	[SerializeField]
	private int m_endWave;

	[SerializeField]
	private int m_waveSpawnCount;
	[SerializeField]
	private int m_waveDeadCount;


	private int m_wave;
	private int m_enmeyAmount = 3;

	private int m_waveSpawnAmount = 10;

	private float[,] m_randomSpawnTable = { { 0.8f, 0.2f, 0.0f }, { 0.5f, 0.4f, 0.1f }, { 0.3f, 0.5f, 0.2f } };

	private Transform m_playerTransform;


	private float m_spawnTick;
	private float m_currentTick;
	private bool m_isWaveSystemOn;

	private EnemeyManager m_enemyManager;


	private List<Enemy> m_waveEnemys = new();

	private void Start()
	{
		m_playerTransform = GameObject.Find("Player").transform;
		m_enemyManager = GameObject.Find("GameManager").GetComponent<EnemeyManager>();
		EventManager.AddListner<EnemyKill>(WaveEnemyDead);

		SetUp();
		StartWave(0.1f);
	}

	private void Update()
	{
		if (!m_isWaveSystemOn || IsWaveEnemyAllSpawn())
			return;

		m_currentTick -= Time.deltaTime;
		if (m_currentTick > 0.0f)
			return;

		SpawnEnemy();

	}

	public void SetUp()
	{
		m_wave = 0;
		ResetWaveDate();
	}

	public void StartWave(float tick)
	{
		m_isWaveSystemOn = true;
		m_spawnTick = tick;
	}

	public void SpawnEnemy()
	{
		ObjectPoolKey randomSpawnEnemy = GetRandomSpawnEnemy();
		Vector3 randomSpawnPoint = GetRandomSpawnPoint();

		var spawnEnemy = m_enemyManager.SpawnEnemy(randomSpawnEnemy, randomSpawnPoint);

		m_waveEnemys.Add(spawnEnemy);
		m_waveSpawnCount++;
		m_currentTick = m_spawnTick;
	}

	private void WaveEnemyDead(EnemyKill waveEnemy)
	{
		if(m_waveEnemys.Contains(waveEnemy.enemy))
		{
			m_waveEnemys.Remove(waveEnemy.enemy);
			m_waveDeadCount++;
			if (IsWaveEnemyAllDead())
				NextWave();
		}
	}

	private void ResetWaveDate()
	{
		m_waveSpawnCount = 0;
		m_waveDeadCount = 0;
	}


	private ObjectPoolKey GetRandomSpawnEnemy()
	{
		float r = Random.Range(0.0f, 1.0f);
		float t = 0.0f;
		ObjectPoolKey spawnEnemy = ObjectPoolKey.Enemy_Normal;
		for (int i = 0; i < m_enmeyAmount; i++)
		{
			t += m_randomSpawnTable[m_wave, i];
			if (t >= r)
			{
				spawnEnemy = (ObjectPoolKey)((int)ObjectPoolKey.Enemy_Normal + i);
				break;
			}
		}
		return spawnEnemy;
	}

	private Vector3 GetRandomSpawnPoint()
	{
		Vector3 spawnPoint = Vector3.zero;
		float randomDistance = Random.Range(m_deadRange, m_spawnRange);
		float randomTheta = Random.Range(0.0f, Mathf.PI * 2.0f);
		spawnPoint = new Vector3(Mathf.Cos(randomTheta), 0.0f, Mathf.Sin(randomTheta));
		spawnPoint *= randomDistance;
		spawnPoint += m_playerTransform.position;

		return spawnPoint;
	}

	private void NextWave()
	{
		m_wave++;
		if (m_wave == m_endWave)
		{
			EventManager.Broadcast(Events.GameClear);
			m_isWaveSystemOn = false;
		}
		else
		{
			//Debug.Log("Next Wave");
			ResetWaveDate();
		}
	}

	private bool IsWaveEnemyAllDead()
	{
		return IsWaveEnemyAllSpawn() && m_waveSpawnCount == m_waveDeadCount;
	}

	private bool IsWaveEnemyAllSpawn()
	{
		return m_waveSpawnAmount == m_waveSpawnCount;
	}

	private void OnDrawGizmos()
	{
		if (!m_isDebug)
			return;

		Vector3 p = GameObject.Find("Player").transform.position;

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(p, m_deadRange);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (p, m_spawnRange);
	}
}
