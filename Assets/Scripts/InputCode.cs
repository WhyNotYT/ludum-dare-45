using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class InputCode : MonoBehaviour
{

	public TMPro.TMP_InputField inputField;
	public Button compileButton;

    void Start()
    {
		inputField.text = "\n\n\n\n\nReturnToBase\nSleep";
    }

	private void Update()
	{
		
	}

	public bool ErrorCheck(string[] code, out string errorCode, out int lineNumber)
	{
		
		bool error = false;
		for (int i = 0; i < code.Length; i++)
		{
			if(code[i].ToLower().Contains("lookforplant"))
			{
				error = false;
				continue;
			}
			else if(code[i].ToLower().Contains("lookformetal"))
			{
				error = false;
				continue;
			}
			else if (code[i].ToLower().Contains("lookforrock"))
			{
				error = false;
				continue;
			}
			else if (code[i].ToLower().Contains("loop:"))
			{
				int a = 0;
				if(Int32.TryParse(code[i].Split(':')[1], out a))
				{
					error = false;
					continue;
				}
				else
				{
					error = true;
					errorCode = "Loop Statement Contains Invalid Line Number."; lineNumber = i;
					break;
				}
			}
			else if (code[i].ToLower().Contains("goto:"))
			{
				int a = 0;
				if (Int32.TryParse(code[i].Split(':')[1], out a))
				{
					error = false;
					continue;
				}
				else
				{
					error = true;
					errorCode = "Goto Statement Contains Invalid Line Number."; lineNumber = i;
					break;
				}
			}
			else if (code[i].ToLower().Contains("mine"))
			{
				if(code[i - 1].ToLower().Contains("lookfor"))
				{
					error = false;
					continue;
				}
				else
				{
					error = true;
					errorCode = "Nothing To Mine."; lineNumber = i;
					break;
				}
			}
			else if (code[i].ToLower().Contains("sleep"))
			{
				error = false;
				continue;
			}
			else if(string.IsNullOrEmpty(code[i]))
			{
				error = false;
				continue;
			}
			else
			{
				error = true;
				errorCode = "Invalid Statement"; lineNumber = i;
				break;
			}
		}
		errorCode = null;
		lineNumber = -1;
		return error;
	}


	public void Compile()
	{
		string[] code = inputField.text.Split('\n');
		Debug.Log(code);
		string errorCode = "";
		int lineNumber = 0;
		bool errorCheckFailed = ErrorCheck(code, out errorCode, out lineNumber);

		Debug.Log("Error: " + errorCheckFailed);
		if(!errorCheckFailed)
		{
		}
		else
		{

		}


		FindObjectOfType<Player>().Sleeping = false;
		FindObjectOfType<Player>().dayCode = code;
		FindObjectOfType<Player>().currentLine = 0;
		FindObjectOfType<Player>().execute();

		inputField.DeactivateInputField();

		FindObjectOfType<InventoryManager>().startTime = Time.time;

		FindObjectOfType<InventoryManager>().dayProgress = 0;

		Debug.Log("reeeeeched End");

	}




}
