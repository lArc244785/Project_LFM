using UnityEngine;

public enum ClickType
{
	Down,
	Press,
	Up,
}


public class PlayerInput : MonoBehaviour
{
	private IMovable m_playerMove;
	private WeaponManager m_weaponManager;
	private WeaponConroller m_conroller;
	private GameManager m_gameManager;
	private JoyStick m_moveJoyStick;
	private JoyStick m_attackJoyStick;

	public bool IsDebug;

	private bool m_isMousePress;

	public bool IsControl { set; get; }

	private Vector3 m_input;

	private void Awake()
	{
		m_playerMove = GetComponent<PlayerMovement>();
		m_weaponManager = GetComponent<WeaponManager>();
		m_conroller = GetComponent<WeaponConroller>();
		m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		m_moveJoyStick = GameObject.Find("MoveJoyStick").GetComponent<JoyStick>();
		m_attackJoyStick = GameObject.Find("AttackJoyStick").GetComponent<JoyStick>();
		GetComponent<Health>().OnDead += () => OnMove(Vector3.zero);

		//if (Application.platform == RuntimePlatform.Android)
		m_moveJoyStick.OnInputChange += OnMove;

		IsControl = true;
	}

	public void OnMove(Vector3 input)
	{
		input = new Vector3(input.z, 0.0f, -input.x);
		var rotationInput = Quaternion.AngleAxis(-45.0f, Vector3.up) * input;

		input = rotationInput.normalized;
		m_playerMove.MoveDir = input;
	}

	private void Update()
	{
		if (m_gameManager.State == GameState.GameOver)
		{
			if (Input.anyKeyDown)
				m_gameManager.Continue();

			return;
		}

		if (!IsControl)
			return;

		//PC
		//if (Application.platform != RuntimePlatform.Android)
		//{
		//	float z = Input.GetAxisRaw("Vertical");
		//	float x = Input.GetAxisRaw("Horizontal");
		//	m_input = new Vector3(x, 0, z);
		//	OnMove(m_input);
		//}

		if (m_attackJoyStick.IsDown)
		{
			m_conroller.HandleFire(ClickType.Down);
			m_isMousePress = true;
		}
		else 
		{
			m_conroller.HandleFire(ClickType.Up);
			m_isMousePress = false;
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			m_conroller.HandleReload();
		}

	}

	public void OnReLoad()
	{
		m_conroller.HandleReload();
	}



}
