using TMPro;
using UnityEngine;
public class HealthInfoGUI : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI m_hp;

	private Health m_health;

	public void Init(Health health)
	{
		m_health = health;
		m_health.OnHit += DrawGUI;
		m_health.OnDead += DrawGUI;
		DrawGUI();
	}

	private void DrawGUI()
	{
		m_hp.text = m_health.HP.ToString();
	}

	private void OnDestroy()
	{
		if (m_health == null)
			return;

		m_health.OnHit -= DrawGUI;
		m_health.OnDead -= DrawGUI;
	}
}
