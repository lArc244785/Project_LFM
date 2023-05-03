using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField]
	private float mSpeed;
	[SerializeField]
	private int mDamage;
	[SerializeField]
	private LayerMask mMask;

	private Rigidbody mRig;

	private void Start()
	{
		mRig = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		mRig.velocity = transform.forward * mSpeed;
	}

	private void OnCollisionEnter(Collision collision)
	{
		var damagable = collision.gameObject.GetComponent<Damagable>();
		if (damagable == null)
			return;

		damagable.OnDamge(mDamage);
		Destroy(gameObject);
	}
}
