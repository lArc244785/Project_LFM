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

	private Weapon m_weapon;

	private void UpdateAmmo()
	{
		m_ammo.text = $"{m_weapon.Ammo} / {m_weapon.MaxAmmo}";
	}

	private void EmptyAmmo()
	{
		m_ammo.text = "Empty";
	}

	private void ReloadStart()
	{
		m_ammo.text = "Reloading";
	}

	private void ReloadEnd()
	{
		UpdateAmmo();
	}

	public void SetWeapon(Weapon weapon)
	{
		m_weapon = weapon;
		m_weapon.OnFire += UpdateAmmo;
		m_weapon.OnReloadStart += ReloadStart;
		m_weapon.OnReloadEnd += ReloadEnd;
		m_weapon.OnEmpty += EmptyAmmo;

		UpdateWeapon();
	}

	public void ReleseWeapon()
	{
		if (m_weapon == null)
			return;

		m_weapon.OnFire -= UpdateAmmo;
		m_weapon.OnReloadStart -= ReloadStart;
		m_weapon.OnReloadEnd -= ReloadEnd;
		m_weapon.OnEmpty -= EmptyAmmo;
	}

	private void UpdateWeapon()
	{
		m_weaponName.text = m_weapon.name;
		UpdateAmmo();
	}

}
