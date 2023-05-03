using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
	[SerializeField]
	private Transform mAimTransfrom;

	public Vector3 AimForward { get => mAimTransfrom.forward; }
	public Vector3 AimPoint { get => mAimTransfrom.position; }
	
}
