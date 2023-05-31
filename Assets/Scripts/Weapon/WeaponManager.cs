using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponIndex
{
	MachineGun,
	//ShotGun,
	//MaxMachineGun,
	Total
}

public class WeaponManager : MonoBehaviour
{
	private PlayerInputHandler m_inputHandler;
	private Weapon m_currentWeapon;

	private Weapon[] m_weapons;

	private WeaponInfoGUI m_weaponInfoGUI;


	public void Init(PlayerInputHandler inputHandler)
	{
		m_inputHandler = inputHandler;
		m_weaponInfoGUI = GameObject.Find("WeaponInfo").GetComponent<WeaponInfoGUI>();

		m_weapons = transform.Find("WeaponPivot").GetComponentsInChildren<Weapon>();
		foreach (Weapon weapon in m_weapons)
			weapon.gameObject.SetActive(false);

		if (m_weapons.Length != (int)WeaponIndex.Total)
		{
			Debug.LogError($"missing weapon ammount {m_weapons.Length} {(int)WeaponIndex.Total}");
			return;
		}

		SetWeapon(WeaponIndex.MachineGun);
	}


	private void Update()
	{
		//Fire
		if (m_inputHandler.GetFireInputDown() || m_inputHandler.GetFireInputHeld())
			m_currentWeapon.Fire();
		//Reload
		if (m_inputHandler.GetReload())
			m_currentWeapon.Reload();
	}

	public void SetWeapon(WeaponIndex index)
	{
		if(m_currentWeapon != null)
		{
			m_weaponInfoGUI.ReleseWeapon();
			m_currentWeapon.gameObject.SetActive(false);
		}

		m_currentWeapon = m_weapons[(int)index];
		m_currentWeapon.gameObject.SetActive(true);
		m_currentWeapon.Init();
		m_weaponInfoGUI.SetWeapon(m_currentWeapon);
	}
}
