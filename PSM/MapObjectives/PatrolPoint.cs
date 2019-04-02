using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour 
{
	public static List<PatrolPoint> PatrolList = new List<PatrolPoint>();

	[SerializeField]
	private float _debugRadius = 1.0f;

	private void OnEnable()
	{
		PatrolList.Add(this);
	}

	private void OnDisable()
	{
		PatrolList.Remove(this);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,_debugRadius);
	}
}
