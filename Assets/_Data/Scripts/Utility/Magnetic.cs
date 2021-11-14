using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Magnetic : MonoBehaviour
{
	[SerializeField] Transform magnetTransform; // This script may be on a child because of potentially needing two trigger colliders
	[SerializeField] string tagAttractedTo = "Player";
	[SerializeField] float attractionPower = 1f;

	Transform target;

	void Update()
	{
		if (target)
		{
			float step = attractionPower * Time.deltaTime;
			magnetTransform.position = Vector3.MoveTowards(magnetTransform.position, target.position, step);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == tagAttractedTo)
		{
			target = other.transform;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == tagAttractedTo)
		{
			target = null;
		}
	}
}