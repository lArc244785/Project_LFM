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

	public Vector3 Input { get; private set; }

	[SerializeField]
	private bool m_isPivotRight;
	private int m_xPos = -1;

	public bool IsDown { private set; get; }

	private void Start()
	{
		m_joystickPivotPos = m_joystickRectTransfrom.anchoredPosition;
		if (!m_isPivotRight)
			m_xPos = 1;
	}


	public void OnDrag(PointerEventData eventData)
	{
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
			m_areaRectTransfrom,
			eventData.position,
			eventData.pressEventCamera,
			out var inputPos))
		{
			inputPos.x = inputPos.x / m_areaRectTransfrom.sizeDelta.x;
			inputPos.y = inputPos.y / m_areaRectTransfrom.sizeDelta.y;
			inputPos = new Vector2(inputPos.x * 2 + m_xPos, inputPos.y * 2 - 1);

			if (inputPos.magnitude > 1)
				inputPos = inputPos.normalized;

			inputPos = new Vector2(
				inputPos.x * (m_areaRectTransfrom.sizeDelta.x * 0.25f),
				inputPos.y * (m_areaRectTransfrom.sizeDelta.y * 0.25f));

			m_joystickRectTransfrom.anchoredPosition = inputPos;

			Vector3 input = new Vector3(inputPos.x, 0.0f, inputPos.y);

			Input = input;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		m_joystickRectTransfrom.anchoredPosition = m_joystickPivotPos;
		Input = Vector3.zero;
		IsDown = false;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("OnPointerDown");
		IsDown = true;
	}
}
