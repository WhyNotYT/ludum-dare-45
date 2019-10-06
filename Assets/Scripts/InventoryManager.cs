using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;





public class InventoryManager : MonoBehaviour
{
	public float resourceUpdateRate;
	public TMP_Text organicCounter;
	public TMP_Text stoneCounter;
	public TMP_Text metalCounter;
	public TMP_Text energyCounter;
	public Image energyBar;
	public Image timeCounter;
	public float dayLength;
	public bool Paused;
	public float energyTickRate;

	public int origanicMatter;
	public int stones;
	public int metals;
	public float energy;
	
	private float energyTickCounter;
	public float dayProgress;
	public float startTime;


	public GameObject EndOfDayUI;
	public TMP_InputField inputField;
	public SpriteRenderer night;
	public bool gameCompleted;


	private Player player;
	private float dayTimeCounter;
	private bool CriticalEnergySleep;
	

	private void Start()
	{
		player = FindObjectOfType<Player>();
		InvokeRepeating("UpdateTexts", 0, resourceUpdateRate);
		dayTimeCounter = 50;
	}



	private void Update()
	{
		Debug.Log(dayTimeCounter);
		/*
		if(dayProgress >= 1)
		{
			EndOfDayUI.SetActive(true);
			//inputField.ActivateInputField();
			inputField.enabled = true;
			player.Sleeping = true;
			player.taskCompleted = true;
			Time.timeScale = 1;
		}
		*/
		if (!Paused)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				ToggleEndOfDayUI(true);
			}
			night.color = Color.black * Mathf.Clamp01(((Mathf.Sin(dayTimeCounter * dayLength) * 0.5f) + 0.3f)) * 0.7f;
			dayTimeCounter += Time.deltaTime;
		}
		else
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				ToggleEndOfDayUI(false);
			}
		}

		if (energy <= 0)
		{
			Debug.Log("Slept");
			player.Sleeping = true;
			CriticalEnergySleep = true;
		}
		if (CriticalEnergySleep)
		{
			if (energy > 10)
			{
				CriticalEnergySleep = false;
				player.Sleeping = false;
			}
		}
	}


	void UpdateTexts()
	{
		energy = Mathf.Clamp(energy, -1, 100);
		organicCounter.text = "Organic Matter: " + origanicMatter.ToString();
		stoneCounter.text = "Resin: " + stones.ToString();
		metalCounter.text = "Metal: " + metals.ToString();
		energyCounter.text = "Energy: " + Mathf.Round(energy).ToString() + "%";
		
		energyBar.fillAmount = (energy / 100f);

	}
	

	public void ToggleEndOfDayUI(bool value)
	{
		EndOfDayUI.SetActive(value);
		Paused = value;
	}



	public void setTimeScale(float value)
	{
		Time.timeScale = value;
	}
}
