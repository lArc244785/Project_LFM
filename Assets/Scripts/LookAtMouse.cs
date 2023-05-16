using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private Actor m_actor;
	private Vector3 m_worldPos;
	private Plane m_plane = new Plane(Vector3.up, 0);
	
	private void Start()
	{
		m_actor = GetComponent<Actor>();
	}

	// Update is called once per frame
	void Update()
    {
		if (!TryGetMouseWorldPos(out var targetPos))
			return;

		Vector3 dir = targetPos - m_actor.transform.position;
		dir.y = 0;

		Quaternion rot = Quaternion.LookRotation(dir.normalized);

		m_actor.transform.rotation = rot;
		
    }

	private bool TryGetMouseWorldPos(out Vector3 worldPos)
	{
		worldPos = Vector3.zero;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (m_plane.Raycast(ray, out var distance))
		{
			worldPos = ray.GetPoint(distance);
			return true;
		}

		return false;
	}


}
