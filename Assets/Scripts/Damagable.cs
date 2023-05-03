using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Damagable : MonoBehaviour
{
	private Health mHealth;

	private void Awake()
	{
		mHealth = GetComponent<Health>();
	}


	public void OnDamge(int damage)
	{
		mHealth.TakeDamage(damage);
	}
}
