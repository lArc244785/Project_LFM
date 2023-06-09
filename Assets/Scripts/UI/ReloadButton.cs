using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ReloadButton : MonoBehaviour
{
	private Button m_button;
	private PlayerInputHandler m_playerInputHandler;
	
	private void Start()
	{
		m_button = GetComponent<Button>();
		m_playerInputHandler = FindObjectOfType<PlayerInputHandler>();

		m_button.onClick.AddListener(m_playerInputHandler.OnClickReload);
	}

}
