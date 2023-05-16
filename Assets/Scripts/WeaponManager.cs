using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
	[SerializeField]
	private List<WeaponBase> m_weaponList = new List<WeaponBase>();
	private WeaponConroller m_conroller;
	private WeaponInfoGUI m_infoGUI;

	private void Start()
	{
		m_conroller = GetComponent<WeaponConroller>();
		m_infoGUI = GameObject.FindObjectOfType<WeaponInfoGUI>();

		foreach (var weapon in m_weaponList)
			weapon.Init();

		ChangeWeapon(0);
	}

	public void ChangeWeapon(int slotIndex)
	{
		if(!m_conroller.CanChangeWeapon())
			return;

		m_conroller.WeaponVisable(false);
		m_conroller.SetWeapon(m_weaponList[slotIndex]);
		m_conroller.WeaponVisable(true);

		m_infoGUI.ChangeWeapon(m_conroller.HandleWeapon);
	}
}
