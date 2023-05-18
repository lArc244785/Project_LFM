using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMovement : MonoBehaviour,IMovable
{
	private bool mIsMoveAble;
	private NavMeshAgent mAgent;

	public Transform Target;

	public float Speed { get => mAgent.speed; set => mAgent.speed = value; }
	public bool IsMoveAble { get => mIsMoveAble; set => mIsMoveAble = value; }
	public Vector3 MoveDir { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

	private void Start()
	{
		mAgent = GetComponent<NavMeshAgent>();
		Target = GameObject.Find("Player").transform;
	}

	private void Update()
	{
		mAgent.SetDestination(Target.position);
	}

}
