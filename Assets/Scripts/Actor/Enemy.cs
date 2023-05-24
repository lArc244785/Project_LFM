using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Health Health { private set; get; }

	private LayerMask m_target;
	[SerializeField]
	private int m_damage;

	public void Init()
	{
		m_target = LayerMask.GetMask("Player");
		Health = GetComponent<Health>();
		Health.Init(10);
		Health.OnDead += Dead;
	}

	private void Dead()
	{
		Health.OnDead -= Dead;
		Destroy(gameObject);
	}
	private void OnTriggerEnter(Collider other)
	{
		int player = 1 << other.gameObject.layer;

		if ((m_target & player) == 0)
			return;

		other.gameObject.GetComponent<Health>().TakeDamage(m_damage);
	}
}
