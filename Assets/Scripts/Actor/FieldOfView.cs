using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
	[SerializeField]
	private bool m_isDebug;
	[SerializeField]
	private float m_viewAngle;
	[SerializeField]
	private float m_range;
	[SerializeField]
	private LayerMask m_targetMask;

	private float m_lookAngle;

	private Vector3 m_lookDir;
	private Vector3 m_lDir;
	private Vector3 m_rDir;


	public void SetRange(float range)
	{
		m_range = range;
	}

	public bool TryGetMinDistanceActor(out Actor actor)
	{
		ChangeLookDir();
		Collider[] enemys = Physics.OverlapSphere(transform.position, m_range, m_targetMask);
		actor = null;

		if (enemys.Length < 0)
			return false;

		float minDistance = Mathf.Infinity;
		foreach (Collider c in enemys)
		{
			Vector3 t = c.transform.position - transform.position;
			float distance = t.magnitude;
			float tAngle = Mathf.Acos(Vector3.Dot(m_lookDir, t.normalized)) * Mathf.Rad2Deg;
			if (tAngle <= (m_viewAngle * 0.5f))
			{
				if (minDistance > distance)
				{
					minDistance = distance;
					actor = c.GetComponent<Actor>();
				}
			}
		}

		if (actor == null)
			return false;

		return true;
	}

	private void ChangeLookDir()
	{
		m_lookAngle = transform.rotation.eulerAngles.y;
		m_lDir = GetAngleToDir(m_lookAngle - (m_viewAngle * 0.5f));
		m_rDir = GetAngleToDir(m_lookAngle + (m_viewAngle * 0.5f));
		m_lookDir = GetAngleToDir(m_lookAngle);
	}


	private void OnDrawGizmos()
	{
		if (!m_isDebug)
			return;

		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, m_range);
		ChangeLookDir();
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, transform.position + (m_lookDir * m_range));
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, transform.position + (m_lDir * m_range));
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + (m_rDir * m_range));
		
		if (TryGetMinDistanceActor(out var actor))
		{
			Gizmos.color = Color.white;
			Gizmos.DrawLine(transform.position, actor.transform.position);
		}
	}

	private Vector3 GetAngleToDir(float angle)
	{
		float radian = angle * Mathf.Deg2Rad;
		return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
	}
}
