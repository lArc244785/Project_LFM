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

	private ActorFlash m_actorFlash;

	private float m_ghostTime;

	private void Start()
	{
		m_health = GetComponent<Health>();
		m_weaponManager = GetComponent<WeaponManager>();
		m_buffSystem = GetComponent<BuffSystem>();
		m_actor = GetComponent<Actor>();
		m_playerMovement = GetComponent<PlayerMovement>();
		m_playerInputHandler = GetComponent<PlayerInputHandler>();
		m_fieldOfView = GetComponent<FieldOfView>();
		m_actorFlash = GetComponent<ActorFlash>();

		m_weaponManager.Init(m_playerInputHandler, m_fieldOfView, m_actor);

		m_health.Init();
		Events.PlayerHeathUpdate.PlayerHealth = m_health;
		m_health.OnDead += OnDead;
		m_health.OnHit += OnHit;
		m_health.OnHit += OnGhost;
		EventManager.Broadcast(Events.PlayerHeathUpdate);
	}

	private void Update()
	{
		if (!CanControl())
			return;

		Ghost();

		m_playerMovement.Move(m_playerInputHandler.GetMoveDir());
		transform.rotation = m_playerInputHandler.GetRotaion(transform.rotation);
	}

	private bool CanControl()
	{
		return GameFlowManager.State == GameState.Play;
	}

	private void OnDead()
	{
		m_health.IsGodMode = true;
		EventManager.Broadcast(Events.GameOver);
	}

	private void OnHit()
	{
		EventManager.Broadcast(Events.PlayerHeathUpdate);
	}

	private void OnGhost()
	{
		m_ghostTime = 0.2f;
		m_health.IsGodMode = true;
	}

	private void Ghost()
	{
		if (m_ghostTime > 0.0f)
		{
			m_ghostTime -= Time.deltaTime;
		}
		else
		{
			m_health.IsGodMode = false;
		}
	}
}
