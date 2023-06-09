using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
	public float Speed { get; set; }
	public Vector3 MoveDir { get; set; }
	public bool IsMoveAble { get; set; }
}
