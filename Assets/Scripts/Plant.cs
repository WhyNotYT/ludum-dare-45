using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
	public float Health = 10;
	public ParticleSystem particle;
	public int organicMatterPerTick;



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
		inventory.origanicMatter += organicMatterPerTick;
		Health -= 1;
		particle.Emit(1);
	}

}
