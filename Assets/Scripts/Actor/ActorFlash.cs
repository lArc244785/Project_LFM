using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorFlash : MonoBehaviour
{
	[SerializeField]
	private MeshRenderer m_model;
	[SerializeField]
	private float m_duration = 0.1f;
	private float m_Intencity = 3.0f;

	private IEnumerator m_OnFlash;

	private Color m_baseColor;

	private void Start()
	{
		m_model.material = Instantiate(m_model.material);
		m_baseColor = m_model.material.color;

		var health = GetComponent<Health>();
		health.OnHit += Flash;
		health.OnDead += ()=> m_model.material.color = m_baseColor;
		GetComponent<Health>().OnHit += Flash;

	}

	public void Flash()
	{
		if (m_OnFlash != null)
			StopCoroutine(m_OnFlash);
		m_OnFlash = OnFlash();
		StartCoroutine(m_OnFlash);
	}

	private IEnumerator OnFlash()
	{
		float t = m_duration;

		while(t > 0)
		{
			var lerp = Mathf.Clamp01(t / m_duration);
			m_model.material.color = m_baseColor * (m_Intencity * lerp);
			t -= Time.deltaTime;
			yield return null;
		}

		m_model.material.color = m_baseColor;
		m_OnFlash = null;
	}

}
