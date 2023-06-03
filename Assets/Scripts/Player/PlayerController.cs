using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
	private Health m_health;

	private WeaponManager m_weaponManager;
	private FieldOfView m_fieldOfView;
	private Actor m_actor;

	private BuffSystem m_buffSystem;

	private PlayerMovement m_playerMovement;
	private PlayerInputHandler m_playerInputHandler;

	private void Start()
	{
		m_health = GetComponent<Health>();
		m_weaponManager = GetComponent<WeaponManager>();
		m_buffSystem = GetComponent<BuffSystem>();
		m_actor = GetComponent<Actor>();
		m_playerMovement = GetComponent<PlayerMovement>();
		m_playerInputHandler = GetComponent<PlayerInputHandler>();
		m_fieldOfView = GetComponent<FieldOfView>();

		m_weaponManager.Init(m_playerInputHandler, m_fieldOfView, m_actor);
		m_health.OnDead += () =>
		{
			m_health.IsGodMode = true;
			EventManager.Broadcast(Events.PlayerLoss);
		};
	}

	private void Update()
	{
		if (!CanControl())
			return; 

		m_playerMovement.Move(m_playerInputHandler.GetMoveDir());
		transform.rotation = m_playerInputHandler.GetRotaion(transform.rotation);
	}

	private bool CanControl()
	{
		return GameManager.State == GameState.Play;
	}

}
