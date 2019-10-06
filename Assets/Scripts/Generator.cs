using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
	public float updateRate;
	public float energyAmount;
	public int organicCostPerTick;

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
		if (inventory.origanicMatter > organicCostPerTick)
		{
			inventory.energy += energyAmount;
			inventory.origanicMatter -= organicCostPerTick;
		}
	}

}
