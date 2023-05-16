using UnityEngine;

public class WeaponConroller : MonoBehaviour
{
	public WeaponBase HandleWeapon { private set; get; }

	public void HandleFire(ClickType type)
	{
		HandleWeapon.Fire(type);
	}

	public void HandleReload()
	{
		HandleWeapon.Reload();
	}

	public bool CanChangeWeapon()
	{
		if (HandleWeapon == null)
			return true;

		return HandleWeapon.CanFire();
	}

	public void SetWeapon(WeaponBase weapon)
	{
		HandleWeapon = weapon;
	}

	public void WeaponVisable(bool isVisable)
	{
		if (HandleWeapon != null)
			HandleWeapon.gameObject.SetActive(isVisable);
	}
}
