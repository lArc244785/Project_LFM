using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PooledObject))]
public class ItemHeal : ItemBase
{
	private Health m_health;

	[SerializeField]
	private int m_heal;


	private void Start()
	{
		m_health = GameObject.Find("Player").GetComponent<Health>();
	}

	public override void Use()
	{
		m_health.Heal(m_heal);
		SoundManager.Instance.Play(SoundID.Item_PickUp_Heal);
		Release();
	}
}
