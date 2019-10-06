using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
	public bool isStatic;
	public Transform target;
	public Vector3 Offset;

	private void Start()
	{
		this.GetComponent<SpriteRenderer>().sprite = target.GetComponent<SpriteRenderer>().sprite;
		if (isStatic)
		{
			this.transform.position = target.position + Offset;
		}
		else
		{
			this.transform.SetParent(null);
		}
	}

	private void Update()
	{
		if (!isStatic)
		{
			this.transform.position = target.position + Offset;
			this.transform.rotation = target.rotation;
		}
	}


}
