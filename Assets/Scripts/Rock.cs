using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
	public float Health = 10;
	public ParticleSystem particle;
	public int resinPerTick;

	private InventoryManager inventory;

	private void Awake()
	{
		this.transform.Rotate(0, 0, Random.Range(0, 360));
		inventory = FindObjectOfType<InventoryManager>();
	}


	private void Update()
	{
		if(Health < 0)
		{
			Destroy(this.gameObject);
		}
	}


	public void Mined()
	{
		Health -= 1;
		inventory.stones += resinPerTick;
		particle.Emit(5);
	}

}
