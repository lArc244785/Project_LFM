using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItem : MonoBehaviour,IItem
{
	private BuffManager m_buffManger;
	private IBuff m_buff;
	private PooledObject m_pooledObject;

	public ObjectPoolKey Key => m_pooledObject.Pool.Type;

	private void Start()
	{
		m_pooledObject  = GetComponent<PooledObject>();
		m_buffManger = GameObject.Find("Player").GetComponent<BuffManager>();
		m_buff = GetComponent<IBuff>();
	}

	public void Use()
	{
		m_buffManger.AddBuff(m_buff);
		m_pooledObject.Release();
	}
}
