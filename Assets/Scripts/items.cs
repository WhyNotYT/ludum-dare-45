using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class building
{
	public bool beingPlaced;
	public GameObject prefab;
	public int oraganicCost;
	public int metalCost;
	public int resinCost;
}



public class items : MonoBehaviour
{
	public building solarPannel;
	public building generator;
	public building printer;
	public building spaceShuttle;
	public bool hasPrinter;
	
	public Image placingObject;


	public AudioSource buildSound;

	private InventoryManager inventory;


	private void Start()
	{
		inventory = FindObjectOfType<InventoryManager>();
	}


	private void Update()
	{
		if(solarPannel.beingPlaced)
		{
			placingObject.sprite = solarPannel.prefab.GetComponentInChildren<SpriteRenderer>().sprite;
			placingObject.transform.position = Input.mousePosition;
			if(Input.GetMouseButtonDown(0))
			{
				Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				/*
				RaycastHit hit;
				if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
				{
					if(hit.collider.name == "buildingsReciever")
					{
						*/
						GameObject solarPannelClone = Instantiate(solarPannel.prefab, targetPosition, Quaternion.identity);
						solarPannelClone.transform.position = new Vector3(solarPannelClone.transform.position.x, solarPannelClone.transform.position.y, 1);
						solarPannel.beingPlaced = false;
						placingObject.transform.position = new Vector3(-100, -100, 0); buildSound.Play();
				//}
				//}
			}
		}

		else if (spaceShuttle.beingPlaced)
		{
			placingObject.sprite = spaceShuttle.prefab.GetComponentInChildren<SpriteRenderer>().sprite;
			placingObject.transform.position = Input.mousePosition;
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				/*
				RaycastHit hit;
				if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
				{
					if(hit.collider.name == "buildingsReciever")
					{
						*/
				GameObject spaceShuttleClone = Instantiate(spaceShuttle.prefab, targetPosition, Quaternion.identity);
				spaceShuttleClone.transform.position = new Vector3(spaceShuttleClone.transform.position.x, spaceShuttleClone.transform.position.y, 1);
				spaceShuttle.beingPlaced = false;
				placingObject.transform.position = new Vector3(-100, -100, 0); buildSound.Play();
				Invoke("gameComplete", 3);
				//}
				//}
			}
		}
		else if (generator.beingPlaced)
		{
			placingObject.sprite = generator.prefab.GetComponentInChildren<SpriteRenderer>().sprite;
			placingObject.transform.position = Input.mousePosition;
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				/*
				RaycastHit hit;
				if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
				{
					if(hit.collider.name == "buildingsReciever")
					{
						*/
				GameObject generatorClone = Instantiate(generator.prefab, targetPosition, Quaternion.identity);
				generatorClone.transform.position = new Vector3(generatorClone.transform.position.x, generatorClone.transform.position.y, 1);
				generator.beingPlaced = false;
				placingObject.transform.position = new Vector3(-100, -100, 0);
				buildSound.Play();
				//}
				//}
			}
		}

		else if (printer.beingPlaced)
		{
			placingObject.sprite = printer.prefab.GetComponentInChildren<SpriteRenderer>().sprite;
			placingObject.transform.position = Input.mousePosition;
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				/*
				RaycastHit hit;
				if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
				{
					if(hit.collider.name == "buildingsReciever")
					{
						*/
				GameObject printerClone = Instantiate(printer.prefab, targetPosition, Quaternion.identity);
				printerClone.transform.position = new Vector3(printerClone.transform.position.x, printerClone.transform.position.y, 1);
				printer.beingPlaced = false;
				placingObject.transform.position = new Vector3(-100, -100, 0);
				hasPrinter = true; buildSound.Play();
				//}
				//}
			}
		}
	}



	public void gameComplete()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Outro");
	}



	public void clickSolarPlannel()
	{
		if (solarPannel.metalCost <= inventory.metals && solarPannel.resinCost <= inventory.stones && solarPannel.oraganicCost <= inventory.origanicMatter)
		{
			solarPannel.beingPlaced = true;
			inventory.metals -= solarPannel.metalCost;
			inventory.stones -= solarPannel.resinCost;
			inventory.origanicMatter -= solarPannel.oraganicCost;
		}
	}


	public void clickPrinter()
	{
		if (printer.metalCost <= inventory.metals && printer.resinCost <= inventory.stones && printer.oraganicCost <= inventory.origanicMatter)
		{
			printer.beingPlaced = true;
			inventory.metals -= printer.metalCost;
			inventory.stones -= printer.resinCost;
			inventory.origanicMatter -= printer.oraganicCost;
		}
	}

	public void clickGenerator()
	{
		if (generator.metalCost <= inventory.metals && generator.resinCost <= inventory.stones && generator.oraganicCost <= inventory.origanicMatter)
		{
			generator.beingPlaced = true;
			inventory.metals -= generator.metalCost;
			inventory.stones -= generator.resinCost;
			inventory.origanicMatter -= generator.oraganicCost;
		}
	}

	public void clickSpaceShuttle()
	{
		if (hasPrinter)
		{
			if (spaceShuttle.metalCost <= inventory.metals && spaceShuttle.resinCost <= inventory.stones && spaceShuttle.oraganicCost <= inventory.origanicMatter)
			{
				spaceShuttle.beingPlaced = true;
				inventory.metals -= spaceShuttle.metalCost;
				inventory.stones -= spaceShuttle.resinCost;
				inventory.origanicMatter -= spaceShuttle.oraganicCost;
			}
		}
	}
}
