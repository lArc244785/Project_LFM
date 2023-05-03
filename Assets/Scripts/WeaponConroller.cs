using UnityEngine;

public class WeaponConroller : MonoBehaviour
{
	private WeaponBase mWepaon;

	public void HandleFire(ClickType type)
	{
		mWepaon.Fire(type);
	}

	public void HandleReload()
	{
		mWepaon.Reload();
	}

	public bool CanChangeWeapon()
	{
		if (mWepaon == null)
			return true;

		return mWepaon.CanFire();
	}

	public void SetWeapon(WeaponBase weapon)
	{
		mWepaon = weapon;
	}

	public void WeaponVisable(bool isVisable)
	{
		if (mWepaon != null)
			mWepaon.gameObject.SetActive(isVisable);
	}
}
