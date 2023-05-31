using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerManager : MonoBehaviour
{
	private Health m_health;
	private WeaponManager m_weaponManager;
	private BuffSystem m_buffSystem;
	private PlayerMovement m_playerMovement;
	private Actor m_actor;
	private PlayerInputHandler m_playerInputHandler;


	private void Start()
	{
		m_health = GetComponent<Health>();
		m_weaponManager = GetComponent<WeaponManager>();
		m_buffSystem = GetComponent<BuffSystem>();
		m_actor = GetComponent<Actor>();
		m_playerMovement = GetComponent<PlayerMovement>();
		m_playerInputHandler = GetComponent<PlayerInputHandler>();

		m_weaponManager.Init(m_playerInputHandler);
	}

	private void Update()
	{
		m_playerMovement.Move(m_playerInputHandler.GetMoveDir());
		transform.rotation = m_playerInputHandler.GetRotaion(transform.rotation);
	}

}
