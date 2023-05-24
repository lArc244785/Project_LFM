using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	private Health m_health;

	private const int m_ghostLayer = 8;
	private const int m_playerLayer = 3;

	private IEnumerator m_ghost;

	private void Awake()
	{
		m_health = GetComponent<Health>();
		m_health.Init(100);
		m_health.OnHit += OnGhost;
	}


	private void OnGhost()
	{
		if (m_ghost != null)
			return;
		m_ghost = HitGhost();
		StartCoroutine(m_ghost);
	}

	private IEnumerator HitGhost()
	{
		gameObject.layer = m_ghostLayer;
		yield return new WaitForSeconds(0.2f);
		gameObject.layer = m_playerLayer;
		m_ghost = null;
	}
}
