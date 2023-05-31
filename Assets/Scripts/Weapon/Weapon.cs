using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState
{
	Firable,
	Reload,
	Empty,
}

public class Weapon : MonoBehaviour
{
	[SerializeField]
	private bool m_isDebug;
	private float m_nextFireAbleTime;
	[SerializeField]
	private float m_fireTick;
	[field:SerializeField]
	public int MaxAmmo { get; private set; }
	public int Ammo { get; private set; }

	[SerializeField]
	private float m_reload;

	[field: SerializeField]
	public float Inaccuracy { get; private set; }
	[field: SerializeField]
	public float Range { get; private set; }

	[SerializeField]
	private Transform m_aimPoint;

	public WeaponState state { private set; get; }

	public event Action OnFire;
	public event Action OnEmpty;
	public event Action OnReloadStart;
	public event Action OnReloadEnd;

	private IEnumerator m_realodCoroutine;

	private ObjectPoolManager m_poolManager;

	private void Start()
	{
		m_poolManager = GameObject.Find("ObjectPoolManager").GetComponent<ObjectPoolManager>();
	}

	public void Init()
	{
		Ammo = MaxAmmo;
		state = WeaponState.Firable;
		m_nextFireAbleTime = 0.0f;

		if(m_realodCoroutine != null)
			StopCoroutine(m_realodCoroutine);
		m_realodCoroutine = null;
	}

	private bool CanFire()
	{
		return state == WeaponState.Firable && 
			m_nextFireAbleTime <= Time.time && 
			Ammo > 0;
	}

	public void Fire()
	{
		if (!CanFire())
			return;
		Debug.Log("Fire");

		var shotDir = GetShotDir();
		var bullet = m_poolManager.GetPooledObject(ObjectPoolKey.Bullet_MG);
		bullet.GetComponent<Bullet>().Init(m_aimPoint, shotDir);

		Ammo--;
		OnFire?.Invoke();

		if (Ammo == 0)
		{
			state = WeaponState.Empty;
			OnEmpty?.Invoke();
		}

		m_nextFireAbleTime = Time.time + m_fireTick;
	}

	public void Reload()
	{
		if (state == WeaponState.Reload)
			return;
		Debug.Log("Reload");

		m_realodCoroutine = ReloadCoroutine();
		StartCoroutine(m_realodCoroutine);
	}

	private IEnumerator ReloadCoroutine()
	{
		state = WeaponState.Reload;
		OnReloadStart?.Invoke();
		yield return new WaitForSeconds(m_reload);
		m_realodCoroutine = null;
		Ammo = MaxAmmo;
		state = WeaponState.Firable;
		OnReloadEnd?.Invoke();
	}

	public Vector3 GetShotDir()
	{
		Vector3 targetPos = m_aimPoint.position + (m_aimPoint.forward * Range);
		targetPos.x += UnityEngine.Random.Range(-Inaccuracy, Inaccuracy);
		targetPos.y += UnityEngine.Random.Range(-Inaccuracy, Inaccuracy);
		targetPos.z += UnityEngine.Random.Range(-Inaccuracy, Inaccuracy);

		Vector3 dir = targetPos - m_aimPoint.position;
		return dir.normalized;
	}


	private void OnDrawGizmos()
	{
		if (!m_isDebug)
			return;

		Gizmos.color = Color.green;

		Vector3 s = m_aimPoint.position;
		Vector3 e = m_aimPoint.position + (m_aimPoint.forward * Range);
		int point = 10;

		float slice = (Mathf.PI * 2) / (point - 1);
		float theta = 0.0f;

		List<Vector3> drawPointList = new();

		for(int i = 0; i < point; i++)
		{
			theta = slice * i;
			Vector3 rotationPoint = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0.0f);
			rotationPoint *= Inaccuracy * 0.5f;
			Vector3 drawPoint = e + rotationPoint;
			drawPointList.Add(drawPoint);
			Gizmos.DrawLine(s, drawPoint);
		}

		for(int i = 1; i < drawPointList.Count; i++)
		{
			Gizmos.DrawLine(drawPointList[i - 1], drawPointList[i]);
		}
	}
}
