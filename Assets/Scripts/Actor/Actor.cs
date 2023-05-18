using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
	[SerializeField]
	private Transform m_aimTransfrom;

	public Vector3 AimForward { get => m_aimTransfrom.forward; }
	public Vector3 AimPoint { get => m_aimTransfrom.position; }
	
}
