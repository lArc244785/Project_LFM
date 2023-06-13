using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{
	[SerializeField]
	private SoundID m_fire;
	[SerializeField]
	private SoundID m_cock;
	[SerializeField]
	private SoundID m_reload;
	[SerializeField]
	private SoundID m_dryFire;


	private void Start()
	{
		var weapon = GetComponent<Weapon>();
		weapon.OnFire += Fire;
		weapon.OnFireFail += DryFire;
		weapon.OnReloadStart += Reload;
		weapon.OnReloadEnd += Cock;
	}

	public void Fire()
	{
		SoundManager.Instance.Play(m_fire);
	}

	public void DryFire()
	{
		SoundManager.Instance.Play(m_dryFire);
	}

	public void Cock()
	{
		SoundManager.Instance.Play(m_cock);
	}

	public void Reload()
	{
		SoundManager.Instance.Play(m_reload);
	}

}
