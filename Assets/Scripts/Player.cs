using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class Player : MonoBehaviour
{
	public float Health = 100;
	public Transform target;
	public Transform safeSpot;
	public float stoppingDistance = 1;
	public float moveSpeed;
	public float miningDistance;
	public float mineInterval;
	public float MoveTickInterval;
	public float vibrationSpeed;
	public float vibrationAmount;


	public int currentLine;
	public string[] dayCode;
	public bool taskCompleted;
	public Objective currentObjective;
	public SpriteRenderer lazer;
	public GameObject warningText;
	public bool Sleeping;
	public AudioSource miningSound;

	private float MoveTickCounter;
	private float mineIntervalCounter;
	private int loopCounter;
	private int loopAmount;
	private int loopStartLine;
	private InventoryManager inventory;
	private bool sleepOnce;

	[Space]

	public float energyCostPerCommand;
	public float energyCostPerMiningTick;
	public float energyCostForMoving;
	


	private void Start()
	{
		inventory = FindObjectOfType<InventoryManager>();
		execute();
	}
	private void Vibrate()
	{
		this.transform.position += new Vector3(Mathf.Sin(Time.time * vibrationSpeed * UnityEngine.Random.Range(0.75f, 1.25f)), Mathf.Sin(Time.time * UnityEngine.Random.Range(0.75f, 1.25f) * vibrationSpeed)) * Time.deltaTime * vibrationAmount;
	}

	private void Update()
	{

		if(!sleepOnce)
		{
			if(Sleeping)
			{
				GameObject warningTextClone = Instantiate(warningText, FindObjectOfType<Canvas>().transform);
				warningTextClone.GetComponent<FadeText>().setText("Drone is Sleeping...\n Press 'Esc' To Update Your Code");
				sleepOnce = true;
			}
		}


		if (!Sleeping)
		{
			sleepOnce = false;
			Vibrate();
			if (!taskCompleted)
			{
				if (currentObjective != null)
				{
					if (currentObjective.type == Type.goTo)
					{
						if (currentObjective.target != null)
						{
							if ((this.transform.position - currentObjective.target.transform.position).sqrMagnitude > stoppingDistance)
							{
								this.transform.Translate(0, moveSpeed * Time.deltaTime, 0);

								Vector3 rotationVector = Vector3.Lerp(this.transform.up, currentObjective.target.transform.position - this.transform.position, 0.05f);
								//rotationVector.x = 0;
								//rotationVector.y = 0;
								this.transform.up = rotationVector;

								if (MoveTickCounter < Time.time)
								{
									inventory.energy -= energyCostForMoving;
									MoveTickCounter = Time.time + MoveTickInterval;
								}
							}
							else
							{
								taskCompleted = true;
							}
						}
						else
						{
							taskCompleted = true;
						}
					}
					else if (currentObjective.type == Type.mine)
					{
						if (currentObjective.target != null)
						{
							if (currentObjective.target.GetComponent<Mineable>() != null)
							{
								if ((this.transform.position - currentObjective.target.transform.position).sqrMagnitude < miningDistance)
								{
									if (mineIntervalCounter < Time.time)
									{

										Vector3 rotationVector = Vector3.Lerp(this.transform.up, currentObjective.target.transform.position - this.transform.position, 0.05f);
										//rotationVector.x = 0;
										//rotationVector.y = 0;
										this.transform.up = rotationVector;

										Debug.Log("Mining...");
										currentObjective.target.GetComponent<Mineable>().Mine();
										lazer.gameObject.SetActive(true);
										mineIntervalCounter = Time.time + mineInterval;
										inventory.energy -= energyCostPerMiningTick;
										if(!miningSound.isPlaying)
										{
											miningSound.Play();
										}
									}
									lazer.transform.Rotate(0, 0, Mathf.Sin(Time.time * 20) * Time.deltaTime * 100);

								}
							}
							else
							{
								currentObjective = null;
								taskCompleted = true;
								lazer.gameObject.SetActive(false);
							}
						}
						else
						{
							currentObjective = null;
							taskCompleted = true;
							lazer.gameObject.SetActive(false);
						}
					}
				}
				else
				{
					taskCompleted = true;
				}
			}
			else
			{
				taskCompleted = false;
				progressToNextLine();
				execute();
			}


		}
		else
		{
			//Time.timeScale = 10;
		}
	}
	public void execute()
	{
		miningSound.Stop();
		inventory.energy -= energyCostPerCommand;
		//Debug.Log(dayCode[currentLine]);
		if(dayCode[currentLine].ToLower().Contains("lookforplant"))
		{
			currentObjective = new Objective();
			currentObjective.type = Type.goTo;

			Plant[] plants = FindObjectsOfType<Plant>();
			float[] distances = new float[plants.Length];
			for (int i = 0; i < plants.Length; i++)
			{
				distances[i] = (this.transform.position - plants[i].transform.position).sqrMagnitude;
			}

			float minDist = Mathf.Min(distances);

			for (int i = 0; i < plants.Length; i++)
			{
				if (minDist == (this.transform.position - plants[i].transform.position).sqrMagnitude)
				{
					currentObjective.target = plants[i].transform;
					break;
				}
			}
			if (currentObjective.target != null)
			{
				Debug.Log("Found Plant:" + currentObjective.target.name);
			}
		}
		else if(dayCode[currentLine].ToLower().Contains("lookforrock"))
		{

			currentObjective = new Objective();
			currentObjective.type = Type.goTo;

			Rock[] rocks = FindObjectsOfType<Rock>();
			float[] distances = new float[rocks.Length];
			for (int i = 0; i < rocks.Length; i++)
			{
				distances[i] = (this.transform.position - rocks[i].transform.position).sqrMagnitude;
			}

			float minDist = Mathf.Min(distances);

			for (int i = 0; i < rocks.Length; i++)
			{
				if (minDist == (this.transform.position - rocks[i].transform.position).sqrMagnitude)
				{
					currentObjective.target = rocks[i].transform;
					break;
				}
			}
		}
		else if(dayCode[currentLine].ToLower().Contains("lookformetal"))
		{

			currentObjective = new Objective();
			currentObjective.type = Type.goTo;

			RockWithMetal[] rocks = FindObjectsOfType<RockWithMetal>();
			float[] distances = new float[rocks.Length];
			for (int i = 0; i < rocks.Length; i++)
			{
				distances[i] = (this.transform.position - rocks[i].transform.position).sqrMagnitude;
			}

			float minDist = Mathf.Min(distances);

			for (int i = 0; i < rocks.Length; i++)
			{
				if (minDist == (this.transform.position - rocks[i].transform.position).sqrMagnitude)
				{
					currentObjective.target = rocks[i].transform;
					break;
				}
			}
		}
		else if(dayCode[currentLine].ToLower().Contains("mine"))
		{
			if (currentObjective.target != null)
			{
				currentObjective.type = Type.mine;
				Debug.Log("Mining Plant:" + currentObjective.target.name);
			}
		}
		else if(dayCode[currentLine].ToLower().Contains("goto:"))
		{
			currentLine = Int32.Parse(dayCode[currentLine].Split(':')[1]);
			execute();
			
		}
		else if(dayCode[currentLine].ToLower().Contains("loop:"))
		{
			loopAmount = Int32.Parse(dayCode[currentLine].Split(':')[1]);
			loopStartLine = currentLine + 1;
			loopCounter = 1;
			progressToNextLine();
			execute();
		}
		else if(dayCode[currentLine].ToLower().Contains("endloop"))
		{
			if(loopCounter < loopAmount)
			{
				currentLine = loopStartLine;
				loopCounter++;
				execute();
			}
			else
			{
				progressToNextLine();
				execute();
			}
		}
		else if(dayCode[currentLine].ToLower().Contains("sleep"))
		{
			Sleeping = true;
		}
		else if(dayCode[currentLine].ToLower().Contains("returntobase"))
		{
			currentObjective = new Objective();
			currentObjective.type = Type.goTo;
			currentObjective.target = safeSpot;
		}
		else
		{
			progressToNextLine();
			execute();
		}

		lazer.gameObject.SetActive(false);
	}


	private void progressToNextLine()
	{
		if (currentLine + 1 < dayCode.Length)
		{
			currentLine++;
		}
		else
		{
			dayCode = new string[0];
			currentLine = 0;
		}
	}
}

public class Objective
{
	public Type type;
	public Transform target;

}

public enum Type
{
	goTo,
	mine
}
