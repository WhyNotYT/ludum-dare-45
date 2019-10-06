/* Copyright (C) Why? Not! YT (Yumish R. Niroula) 2019
	https://www.youtube.com/channel/UC0AkLhy8aP8ns1QGD2lEIuw
*/


using UnityEngine;


public class YT : MonoBehaviour
{
	private Vector3 OriginalScale;


	private void Start()
	{
		OriginalScale = this.transform.localScale;
	}


	private void OnMouseDown()
	{
		//Application.OpenURL(URL);
	}


	public void open(string URL)
	{
		Application.OpenURL(URL);
	}

	private void OnMouseEnter()
	{
		this.transform.localScale *= 1.1f;
	}


	private void OnMouseExit()
	{
		this.transform.localScale = OriginalScale;
	}
}
