using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem 
{
	public ObjectPoolKey Key { get; }
	public void Use();
}
