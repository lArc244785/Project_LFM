using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerInput), typeof(Movement))]
public class Player : MonoBehaviour
{
	private Movement mMoveMent;
	private PlayerInput mInput;

	private void Start()
	{
		mMoveMent = GetComponent<Movement>();
		mInput = GetComponent<PlayerInput>();

		mInput.Init(mMoveMent);
	}
}
