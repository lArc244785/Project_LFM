using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
	public ObjectPool Pool { set; get; }

	public void Release()
	{
		Pool.RetrunToPooledObject(this);
	}
}
