using UnityEngine;

public class WeaponConroller : MonoBehaviour
{
	public WeaponBase HandleWeapon { private set; get; }
	private FieldOfView m_FieldOfView;
	[SerializeField]
	private Transform m_pivot;

	private void Start()
	{
		m_FieldOfView = GetComponent<FieldOfView>();
	}

	public void HandleFire(ClickType type)
	{
		m_pivot.localRotation = Quaternion.identity;

		if(m_FieldOfView.TryGetMinDistanceActor(out var actor))
		{
			Vector3 dir = actor.transform.position - m_pivot.position;
			dir.y = 0;

			Quaternion rot = Quaternion.LookRotation(dir.normalized);
			m_pivot.rotation = rot;
		}

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
