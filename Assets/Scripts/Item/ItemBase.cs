using UnityEngine;

[RequireComponent(typeof(PooledObject))]
public abstract class ItemBase : MonoBehaviour
{
	private PooledObject m_pooledObject;

	private void Awake()
	{
		m_pooledObject = GetComponent<PooledObject>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player")
			return;

		Use();
	}

	public abstract void Use();

	public void Release()
	{
		m_pooledObject.Release();
	}
}
