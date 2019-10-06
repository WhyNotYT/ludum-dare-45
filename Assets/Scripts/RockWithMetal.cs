using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWithMetal : MonoBehaviour
{
	public float Health = 10;
	public ParticleSystem particle;
	public int metalPerTick;

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
		inventory.metals += metalPerTick;
		particle.Emit(5);
	}

}
