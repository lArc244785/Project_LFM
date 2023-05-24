using TMPro;
using UnityEngine;
public class HealthInfoGUI : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI m_hp;

	private Health m_health;
	// Start is called before the first frame update
	void Start()
	{
		m_health = GameObject.FindWithTag("Player").GetComponent<Health>();
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
