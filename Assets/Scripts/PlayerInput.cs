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
	private PlayerMovement mMovement;
	private WeaponManager mWeaponManager;
	private WeaponConroller mConroller;

	public bool isDebug;

	private bool mIsMousePress;

	private void Awake()
	{
		mMovement = GetComponent<PlayerMovement>();
		mWeaponManager = GetComponent<WeaponManager>();
		mConroller = GetComponent<WeaponConroller>();
	}

	public void OnMove(Vector3 input)
	{
		input = input.normalized;
		mMovement.Move(input);
	}

	private void Update()
	{
		var z = Input.GetAxisRaw("Vertical");
		var x = Input.GetAxisRaw("Horizontal");

		var input = new Vector3(z, 0.0f, -x);
		var rotationInput = Quaternion.AngleAxis(-45.0f, Vector3.up) * input;

		if (isDebug)
			Debug.Log(rotationInput);

		OnMove(rotationInput);


		if (Input.GetMouseButtonDown(0))
		{
			mConroller.HandleFire(ClickType.Down);
			mIsMousePress = true;
		}
		else if(mIsMousePress)
		{
			mConroller.HandleFire(ClickType.Press);
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			mConroller.HandleFire(ClickType.Up);
			mIsMousePress = false;
		}

		if(Input.GetKeyDown(KeyCode.R))
		{
			mConroller.HandleReload();
		}

	}



}
