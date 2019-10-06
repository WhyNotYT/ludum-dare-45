using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPannel : MonoBehaviour
{
	public float updateRate;
	public float energyAmount;


	private InventoryManager inventory;

	private float updateCounter;


	private void Start()
	{
		inventory = FindObjectOfType<InventoryManager>();
		
	}



	private void Update()
	{
		if (!inventory.EndOfDayUI.activeInHierarchy)
		{
			if (updateCounter < Time.time)
			{
				harvest();
				updateCounter = Time.time + updateRate;
			}
		}
	}

	void harvest()
	{
		if (inventory.night.color.a < 0.4f)
		{
			inventory.energy += energyAmount * (1- inventory.night.color.a);
		}
	}

}
