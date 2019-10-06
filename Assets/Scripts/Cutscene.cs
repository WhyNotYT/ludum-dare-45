using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;


public class Cutscene : MonoBehaviour
{
	public string levelName;
	public VideoPlayer videoPlayer;
	public string videoName;

	private void Awake()
	{
		//videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoName);
		videoPlayer.Play();
	}


	private void Update()
	{
		if(Time.timeSinceLevelLoad > videoPlayer.clip.length)
		{
			SceneManager.LoadScene(levelName);
		}
	}


	public void skip()
	{

		SceneManager.LoadScene(levelName);
	}

}
