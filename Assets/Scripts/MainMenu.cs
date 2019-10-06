using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public GameObject tutorialPopUp;

	public void startPressed()
	{
		tutorialPopUp.SetActive(true);
	}


	public void loadScene(string name)
	{
		SceneManager.LoadScene(name);
	}


	public void Quit()
	{
		Application.Quit();
	}
}
