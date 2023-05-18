using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
	private List<IBuff> m_buffList = new();

	private void Update()
	{
		var time = Time.time;

		while (m_buffList.Count > 0 && m_buffList[0].EndTime <= time)
		{
			m_buffList[0].Exit();
			m_buffList.RemoveAt(0);
		}
	}

	public void AddBuff(IBuff newBuff)
	{
		bool isOverlap = false;
		int index = 0;

		while(index < m_buffList.Count && !isOverlap)
		{
			if (m_buffList[index].Type == newBuff.Type)
			{
				m_buffList[index].Exit(true);
				m_buffList.RemoveAt(index);
				isOverlap = true;
			}
			index++;
		}

		newBuff.EndTime = Time.time + newBuff.Duration;
		newBuff.Enter();
		m_buffList.Add(newBuff);
	}

	public void ResetBuff()
	{
		foreach (var buff in m_buffList)
		{
				buff.Exit();
		}

		m_buffList.Clear();
	}
}
