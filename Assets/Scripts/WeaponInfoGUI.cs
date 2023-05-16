using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoGUI : MonoBehaviour
{
	[SerializeField]
	private Image m_Icon;
	[SerializeField]
	private TextMeshProUGUI m_weaponName;
	[SerializeField]
	private TextMeshProUGUI m_ammo;

	private WeaponBase m_weapon;

	private void UpdateAmmon()
	{
		m_ammo.text = $"{m_weapon.Ammo} / {m_weapon.MaxAmmo}";
	}

	private void StartReload()
	{
		m_ammo.text = "Reloading";
	}

	private void FinshReload()
	{
		UpdateAmmon();
	}

	public void ChangeWeapon(WeaponBase weapon)
	{
		if (m_weapon != null)
		{
			m_weapon.OnFire -= UpdateAmmon;
			m_weapon.OnStartReload -= StartReload;
			m_weapon.OnFinshReload -= FinshReload;
		}
		
		m_weapon = weapon;
		m_weapon.OnFire += UpdateAmmon;
		m_weapon.OnStartReload += StartReload;
		m_weapon.OnFinshReload += FinshReload;

		UpdateWeapon();
	}

	private void UpdateWeapon()
	{
		m_weaponName.text = m_weapon.name;
		UpdateAmmon();
	}

}
