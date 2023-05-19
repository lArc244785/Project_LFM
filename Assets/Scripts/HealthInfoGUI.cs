using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
        DrawGUI();
    }
	private void DrawGUI()
	{
        m_hp.text = m_health.HP.ToString();
	}

	private void OnDestroy()
	{
        if (m_health != null)
            m_health.OnHit -= DrawGUI;
    }
}
