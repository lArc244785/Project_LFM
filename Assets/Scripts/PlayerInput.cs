using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	private Movement mMovement;

	public void Init(Movement movement)
	{
		mMovement = movement;
	}

	public void OnMove(Vector3 input)
	{
		input = input.normalized;
		mMovement.Move(input);
	}

	private void Update()
	{
		var z = Input.GetAxis("Vertical");
		var x = Input.GetAxisRaw("Horizontal");

		var input = new Vector3(z, 0.0f, -x);
		var rotationInput = Quaternion.AngleAxis(-45.0f, Vector3.up) * input;

		OnMove(rotationInput);
	}

}
