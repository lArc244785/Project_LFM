using UnityEngine;

public enum WeaponIndex
{
	MachineGun,
	ShotGun,
	//MaxMachineGun,
	Total
}

public class WeaponManager : MonoBehaviour
{
	private PlayerInputHandler m_inputHandler;
	private Weapon m_currentWeapon;

	private Weapon[] m_weapons;

	private WeaponInfoGUI m_weaponInfoGUI;
	private FieldOfView m_fieldOfView;
	private Actor m_actor;

	private IWeaponInfo m_currentWeaponInfo;

	private int m_addRPM;
	public int AddRPM
	{
		set
		{
			m_addRPM = value;
			SetWeaponAddRPM(m_addRPM);
		}
		get
		{
			return m_addRPM;
		}
	}

	private void SetWeaponAddRPM(int addRPM)
	{
		if (m_currentWeapon == null)
			return;
		m_currentWeapon.AddRPM = addRPM;
	}

	public void Init(PlayerInputHandler inputHandler, FieldOfView fov, Actor actor)
	{
		m_inputHandler = inputHandler;
		m_fieldOfView = fov;
		m_actor = actor;
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


	private void LateUpdate()
	{
		//Fire
		if (m_inputHandler.GetFireInputDown() || m_inputHandler.GetFireInputHeld())
		{
			//Auto Amming
			if (m_fieldOfView.TryGetMinDistanceActor(out var target))
			{
				var dir = (target.AimPoint - transform.position).normalized;
				dir.y = 0;
				Quaternion rot = Quaternion.LookRotation(dir);
				transform.rotation = rot;
			}

			if (m_inputHandler.GetFireInputDown())
				m_currentWeapon.FireFail();

			m_currentWeapon.Fire();
		}
		//Reload
		if (m_inputHandler.GetReload())
			m_currentWeapon.Reload();
	}

	public void SetWeapon(WeaponIndex index)
	{
		if (m_currentWeapon != null)
		{
			m_weaponInfoGUI.ReleseWeapon();
			m_currentWeapon.gameObject.SetActive(false);
			m_currentWeaponInfo.AddRPM = 0;
		}

		m_currentWeapon = m_weapons[(int)index];
		m_currentWeaponInfo = m_currentWeapon;
		SetWeaponAddRPM(AddRPM);
		m_currentWeapon.gameObject.SetActive(true);
		m_currentWeapon.Init();
		m_weaponInfoGUI.SetWeapon(m_currentWeapon);
		m_fieldOfView.SetRange(m_currentWeapon.Range);
	}
}
