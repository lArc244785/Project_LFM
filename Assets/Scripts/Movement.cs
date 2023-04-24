using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour,IMovable
{
	private float mSpeed = 10.0f;
	public float Speed { get => mSpeed; set => mSpeed = value; }


	public void Move(Vector3 moveDir)
	{
		//transform.Rotate(moveDir);
		transform.Translate(moveDir * Speed * Time.deltaTime);
	}
}
