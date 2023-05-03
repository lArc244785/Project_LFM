using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour,IMovable
{
	private float mSpeed = 10.0f;
	private bool mIsMoveAble = false;
	public float Speed { get => mSpeed; set => mSpeed = value; }
	public bool IsMoveAble { get => mIsMoveAble; set => mIsMoveAble = value; }

	public void Move(Vector3 moveDir)
	{
		//transform.Rotate(moveDir);
		transform.Translate(moveDir * Speed * Time.deltaTime);
	}
}
