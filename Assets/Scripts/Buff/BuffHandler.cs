using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
	private BuffManager m_buffManger;
	private IBuff m_buff;

	private void Start()
	{
		m_buffManger = GameObject.Find("Player").GetComponent<BuffManager>();
		m_buff = GetComponent<IBuff>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag != "Player")
			return;

		m_buffManger.AddBuff(m_buff);
		Destroy(gameObject);
	}
}
