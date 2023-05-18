using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour,IMovable
{
	private float m_speed = 10.0f;
	private bool m_isMoveAble = false;
	private Vector3 m_moveDir;
	public float Speed { get => m_speed; set => m_speed = value; }
	public bool IsMoveAble { get => m_isMoveAble; set => m_isMoveAble = value; }
	public Vector3 MoveDir { get => m_moveDir; set => m_moveDir = value; }

	protected CharacterController m_characterController;

	private void Start()
	{
		m_characterController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		m_characterController.Move(MoveDir * Time.deltaTime * Speed);
	}
}
