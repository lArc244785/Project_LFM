using System.Collections;
using System.Collections.Generic;
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

	public bool IsDebug;

	private bool m_isMousePress;

	private void Awake()
	{
		m_playerMove = GetComponent<PlayerMovement>();
		m_weaponManager = GetComponent<WeaponManager>();
		m_conroller = GetComponent<WeaponConroller>();
	}

	public void OnMove(Vector3 input)
	{
		input = input.normalized;
		m_playerMove.MoveDir = input;
	}

	private void Update()
	{
		var z = Input.GetAxisRaw("Vertical");
		var x = Input.GetAxisRaw("Horizontal");

		var input = new Vector3(z, 0.0f, -x);
		var rotationInput = Quaternion.AngleAxis(-45.0f, Vector3.up) * input;

		if (IsDebug)
			Debug.Log(rotationInput);

		OnMove(rotationInput);


		if (Input.GetMouseButtonDown(0))
		{
			m_conroller.HandleFire(ClickType.Down);
			m_isMousePress = true;
		}
		else if(m_isMousePress)
		{
			m_conroller.HandleFire(ClickType.Press);
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			m_conroller.HandleFire(ClickType.Up);
			m_isMousePress = false;
		}

		if(Input.GetKeyDown(KeyCode.R))
		{
			m_conroller.HandleReload();
		}

	}



}
