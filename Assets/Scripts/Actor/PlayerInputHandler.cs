using UnityEngine;




public class PlayerInputHandler : MonoBehaviour
{
	private JoyStick m_moveJoyStick;
	private JoyStick m_attackJoyStick;
	private Transform m_playerTransform;
	private Plane m_plane = new Plane(Vector3.up, 0);

	private bool m_isOnClickReload;

	private void Start()
	{
		m_moveJoyStick = GameObject.Find("MoveJoyStick").GetComponent<JoyStick>();
		m_attackJoyStick = GameObject.Find("AttackJoyStick").GetComponent <JoyStick>();
		m_playerTransform = GameObject.Find("Player").transform;
	}

	public bool CanIngameInput()
	{
		return true;
	}
	
	public Vector3 GetMoveDir()
	{
		Vector3 input = Vector3.zero;

		if (!CanIngameInput())
			return input;

#if UNITY_ANDROID
		input = m_moveJoyStick.Input;
#endif
#if UNITY_EDITOR
		input.z = Input.GetAxisRaw(GameConstances.AxisVertical);
		input.x = Input.GetAxisRaw(GameConstances.AxisHorizontal);
#endif
		Vector3 moveDir = new Vector3(input.z, 0.0f, -input.x);
		var rotationInput = Quaternion.AngleAxis(-45.0f, Vector3.up) * moveDir;
		moveDir = rotationInput.normalized;

		return moveDir;
	}
	
	public Quaternion GetRotaion(Quaternion rot)
	{
		if (!CanIngameInput())
			return rot;

		Vector3 rotationDir = Vector3.zero;
#if UNITY_ANDROID
		rotationDir = m_attackJoyStick.Input;
#endif
#if UNITY_EDITOR
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (m_plane.Raycast(ray, out var distance))
		{
			var worldPos = ray.GetPoint(distance);
			var playerToPointDir = worldPos - m_playerTransform.transform.position;
			playerToPointDir.y = 0.0f;
			rotationDir = playerToPointDir.normalized;
		}
#endif

		if (rotationDir.magnitude > 0.0f)
			rot = Quaternion.LookRotation(rotationDir);

		return rot;
	}

	public bool GetFireInputDown()
	{
		if (!CanIngameInput())
			return false;
#if UNITY_EDITOR
		return Input.GetButtonDown(GameConstances.ButtonFire);
#endif
#if UNITY_ANDROID
		return m_attackJoyStick.IsDown;
#endif
	}

	public bool GetFireInputHeld()
	{
		if (!CanIngameInput())
			return false;
#if UNITY_EDITOR
		return Input.GetButton(GameConstances.ButtonFire);
#endif
#if UNITY_ANDROID
		return m_attackJoyStick.IsDown;
#endif
	}

	public bool GetFireInputUp()
	{
		if (!CanIngameInput())
			return false;
#if UNITY_EDITOR
		return Input.GetButtonUp(GameConstances.ButtonFire);
#endif
#if UNITY_ANDROID
		return !m_attackJoyStick.IsDown;
#endif

	}
	
	public bool GetReload()
	{
		if (!CanIngameInput())
			return false;

#if UNITY_EDITOR
		return Input.GetButton(GameConstances.ButtonReload_PC);
#endif
#if UNITY_ANDROID
		return m_isOnClickReload;
#endif
	}

	public void OnClickReload()
	{
		m_isOnClickReload = true;
	}

	private void LateUpdate()
	{
		m_isOnClickReload = false;
	}

}
