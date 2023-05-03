using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	[SerializeField]
	private List<WeaponBase> mWeaponList = new List<WeaponBase>();
	private WeaponConroller mConroller;

	private void Awake()
	{
		mConroller = GetComponent<WeaponConroller>();
		ChangeWeapon(0);
	}

	public void ChangeWeapon(int slotIndex)
	{
		if(!mConroller.CanChangeWeapon())
			return;

		mConroller.WeaponVisable(false);
		mConroller.SetWeapon(mWeaponList[slotIndex]);
		mConroller.WeaponVisable(true);
	}
}
