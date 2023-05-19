using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
	public bool IsDebug;
	public float viewAngle;
	public float range;
	public LayerMask TargetMask;

	private float m_lookAngle;

	private Vector3 m_lookDir;
	private Vector3 m_lDir;
	private Vector3 m_rDir;

	public bool TryGetMinDistanceActor(out Actor actor)
	{
		ChangeLookDir();
		Collider[] enemys = Physics.OverlapSphere(transform.position, range, TargetMask);
		actor = null;

		if (enemys.Length < 0)
			return false;

		float minDistance = Mathf.Infinity;
		foreach (Collider c in enemys)
		{
			Vector3 t = c.transform.position - transform.position;
			float distance = t.magnitude;
			float tAngle = Mathf.Acos(Vector3.Dot(m_lookDir, t.normalized)) * Mathf.Rad2Deg;
			if (tAngle <= (viewAngle * 0.5f))
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
		m_lDir = GetAngleToDir(m_lookAngle - (viewAngle * 0.5f));
		m_rDir = GetAngleToDir(m_lookAngle + (viewAngle * 0.5f));
		m_lookDir = GetAngleToDir(m_lookAngle);
	}


	private void OnDrawGizmos()
	{
		if (!IsDebug)
			return;

		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere(transform.position, range);
		ChangeLookDir();
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, transform.position + (m_lookDir * 10));
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, transform.position + (m_lDir * 10));
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position, transform.position + (m_rDir * 10));
		
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
