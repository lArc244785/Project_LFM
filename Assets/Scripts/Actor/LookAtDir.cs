using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtDir : MonoBehaviour
{
	private Actor m_actor;
	[SerializeField]
	private JoyStick m_attackJoyStick; 


	private void Start()
	{
		m_actor = GetComponent<Actor>();
		m_attackJoyStick.OnInputChange += LookAt;
	}

	public void LookAt(Vector2 dir)
	{
		Vector3 diraction = new Vector3(dir.x, 0, dir.y);

		var calculation = Quaternion.AngleAxis(Camera.main.transform.rotation.eulerAngles.y, Vector3.up) * diraction;

		Quaternion rot = Quaternion.LookRotation(calculation.normalized);

		m_actor.transform.rotation = rot;
	}

	private void OnDestroy()
	{
		m_attackJoyStick.OnInputChange -= LookAt;
	}
}
