using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
	[SerializeField]
	private RectTransform m_areaRectTransfrom;
	[SerializeField]
	private RectTransform m_joystickRectTransfrom;

	private Vector2 m_joystickPivotPos;

	public event Action<Vector2> OnInputChange;

	private void Start()
	{
		m_joystickPivotPos = m_joystickRectTransfrom.anchoredPosition;
	}


	public void OnDrag(PointerEventData eventData)
	{
		Debug.Log("OnDrag");
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
			m_areaRectTransfrom,
			eventData.position,
			eventData.pressEventCamera,
			out var inputPos))
		{
			inputPos.x = inputPos.x / m_areaRectTransfrom.sizeDelta.x;
			inputPos.y = inputPos.y / m_areaRectTransfrom.sizeDelta.y;

			Debug.Log(inputPos);
			inputPos = new Vector2(inputPos.x * 2 + 1, inputPos.y * 2 - 1);
			Debug.Log(inputPos);
			Debug.Log("===================");

			if (inputPos.magnitude > 1)
				inputPos = inputPos.normalized;

			inputPos = new Vector2(
				inputPos.x * (m_areaRectTransfrom.sizeDelta.x * 0.25f),
				inputPos.y * (m_areaRectTransfrom.sizeDelta.y * 0.25f));

			m_joystickRectTransfrom.anchoredPosition = inputPos;

			OnInputChange?.Invoke(inputPos);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		m_joystickRectTransfrom.anchoredPosition = m_joystickPivotPos;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("OnPointerDown");
	}
}
