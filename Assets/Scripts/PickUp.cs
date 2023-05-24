using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		var dropItem = other.GetComponent<IItem>();
		if (dropItem == null)
			return;
		dropItem.Use();
	}
}
