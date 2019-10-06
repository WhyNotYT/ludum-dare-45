using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
	public Transform target;
	public float smoothAmount;
	public Vector3 offset;

	private void Update()
	{
		Vector3 targetVector = Vector2.Lerp(this.transform.position, target.transform.position + offset, smoothAmount);
		targetVector.z = -10;

		this.transform.position = targetVector;
	}

}
