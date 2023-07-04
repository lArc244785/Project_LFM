using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[field:SerializeField]
	public ObjectPoolKey Type { get; private set; }

	private Stack<PooledObject> m_stack = new();
	[SerializeField]
	private PooledObject m_prefab;
	[SerializeField]
	private int m_InitCount;

	public void SetUp()
	{
		if (m_stack.Count > 0)
		{
			foreach (PooledObject obj in m_stack)
			{
				Destroy(obj);
			}
			m_stack.Clear();
		}

		while(m_stack.Count < m_InitCount)
		{
			var po = CreatePooledObject();
			m_stack.Push(po);
		}
	}

	private PooledObject CreatePooledObject()
	{
		var clone = Instantiate(m_prefab,transform);
		clone.gameObject.SetActive(false);
		var po = clone.GetComponent<PooledObject>();
		po.Pool = this;
		return po;
	}

	public GameObject GetPooledObject()
	{
		GameObject pooledObject = null;
		if(m_stack.Count > 0)
		{
			pooledObject = m_stack.Pop().gameObject;
		}
		else
		{
			pooledObject = CreatePooledObject().gameObject;
		}
		pooledObject.SetActive(true);
		return pooledObject;
	}

	public void RetrunToPooledObject(PooledObject po)
	{
		po.gameObject.SetActive(false);
		m_stack.Push(po);
	}
}
