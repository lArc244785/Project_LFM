using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{
	private float m_speed = 10.0f;
	private bool m_isMoveAble = false;
	private Vector3 m_moveDir;
	public float Speed { get => m_speed + AdditionalSpeed.Speed; set => m_speed = value; }
	public bool IsMoveAble { get => m_isMoveAble; set => m_isMoveAble = value; }
	public Vector3 MoveDir { get => m_moveDir; set => m_moveDir = value; }

	private CharacterController m_characterController;
	public AdditionalSpeed AdditionalSpeed { private set; get; } = new();

	private void Start()
	{
		m_characterController = GetComponent<CharacterController>();
		IsMoveAble = true;
	}

	public void Move(Vector3 moveDir)
	{
		if (m_isMoveAble)
			m_characterController.Move(moveDir * Time.deltaTime * Speed);
	}
}
