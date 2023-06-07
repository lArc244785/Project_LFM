using TMPro;
using UnityEngine;
public class HealthInfoGUI : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI m_hp;

	private void Awake()
	{
		EventManager.AddListner<PlayerHeathUpdate>(DrawGUI);
	}

	private void DrawGUI(PlayerHeathUpdate heathUpdate)
	{
		m_hp.text = heathUpdate.PlayerHealth.HP.ToString();
	}
}
