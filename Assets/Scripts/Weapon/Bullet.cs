using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField]
	private float m_speed;
	[SerializeField]
	private int m_damage;
	[SerializeField]
	private LayerMask m_mask;

	private Rigidbody m_rig;

	private BulletType m_bulletType;

	public void Init(BulletType type, int addDamage)
	{
		m_rig = GetComponent<Rigidbody>();
		m_bulletType = type;
		m_damage += addDamage;
	}

	private void Update()
	{
		m_rig.velocity = transform.forward * m_speed;
	}

	private void OnCollisionEnter(Collision collision)
	{
		var damagable = collision.gameObject.GetComponent<Damagable>();
		if (damagable == null)
			return;

		damagable.OnDamge(m_damage);
		Destroy(gameObject);
	}
}
