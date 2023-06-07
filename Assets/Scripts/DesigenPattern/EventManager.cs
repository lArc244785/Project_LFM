using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent { }

public class EventManager:MonoBehaviour
{
	private static Dictionary<Type, Action<GameEvent>> m_events = new();
	private static Dictionary<Delegate, Action<GameEvent>> m_lookUpEvents = new();

	public static void AddListner<T>(Action<T> evt) where T : GameEvent
	{
		if(!m_lookUpEvents.ContainsKey(evt))
		{
			Action<GameEvent> newEvent = (e)=> evt((T)e);
			m_lookUpEvents.Add(evt, newEvent);

			if(m_events.TryGetValue(typeof(T), out Action<GameEvent> internalEvent))
			{
				m_events[typeof(T)] = internalEvent += newEvent;
			}
			else
			{
				Debug.Log($"SUB {evt.GetType()}");
				m_events.Add(typeof(T), newEvent);
			}
		}
	}


	public static void RemoveEvnet<T>(Action<T> evt) where T : GameEvent
	{
		Action<GameEvent> removeEvent = (e) => evt((T)e);
		if (m_lookUpEvents.ContainsKey(removeEvent))
		{
			m_lookUpEvents.Remove(removeEvent);
			if (m_events.TryGetValue(typeof(T), out Action<GameEvent> internalEvent))
			{
				internalEvent -= removeEvent;
			}
		}
	}

	public static void ResetEvent()
	{
		m_events.Clear();
		m_lookUpEvents.Clear();
	}

	public static void Broadcast(GameEvent evt)
	{
		if(m_events.TryGetValue(evt.GetType(), out Action<GameEvent> action))
			action.Invoke(evt);
	}
}
