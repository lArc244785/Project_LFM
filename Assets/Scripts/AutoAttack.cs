using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
	private WeaponConroller m_weaponConroller;
	[SerializeField]
	private float m_range;
	[SerializeField]
	private Color m_debugColor;

	private RaycastHit m_hitTarget;
	private LayerMask m_layerMask;

	private void Start()
	{
		m_weaponConroller = GetComponent<WeaponConroller>();
		m_layerMask = LayerMask.GetMask("Enemy");
	}


	private void OnDrawGizmos()
	{
		Gizmos.color = m_debugColor;
		Gizmos.DrawWireSphere(transform.position, m_range);
	}

	private void Update()
	{
		Physics.SphereCast(transform.position, m_range, Vector3.zero, out m_hitTarget, m_layerMask);
		
		if (m_hitTarget.collider == null)
			return;

		transform.LookAt(m_hitTarget.transform, Vector3.up);
	}
}
