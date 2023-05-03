using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : MonoBehaviour,IMovable
{
	private float mSpeed;
	private bool mIsMoveAble;
	private NavMeshAgent mAgent;

	public float Speed { get => mSpeed; set => mSpeed = value; }
	public bool IsMoveAble { get => mIsMoveAble; set => mIsMoveAble = value; }

	public void Awake()
	{
		mAgent = GetComponent<NavMeshAgent>();
	}


}
